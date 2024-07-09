using System.Transactions;
using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Commans.Attributes;
using Goverment.AuthApi.Commans.Constants;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Goverment.AuthApi.Services.Http;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Goverment.AuthApi.Services.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _jwtService;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly string  _currentUser;
    private readonly IHttpService _httpService;
    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        ITokenHelper token,
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository, IHttpService httpService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = token;
        _currentUser = _jwtService.GetUsername();
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _httpService = httpService;
    }

   // [Transaction]
    public async Task<IDataResponse<CreateUserResponse>> Create(CreateUserRequest createUserRequest, string? organizationName, params string?[]? roles)
    {
        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);
        await EmailIsUnique(createUserRequest.Email);

        HashingHelper.CreatePasswordHash(createUserRequest.Password,
            out var passwordHash, out var passwordSalt);

        List<UserRole> userroleList = [new UserRole { Role = new Role(ROLE_USER.Id, ROLE_USER.Name) }];

        var user = new User
        {
            Email = createUserRequest.Email,
            FullName = createUserRequest.FullName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsVerify = true,
        };

        if (roles != null && roles.Length > 0)
        {
            var  rolesList = await _roleRepository.ListAsync(r => roles.Contains(r.Name));
            if (rolesList.Count() != roles.Length) throw new BusinessException(Messages.RoleDoesNotExists);
            userroleList.AddRange(rolesList.Select(role => new UserRole { Role = role }));
        }


        await _userRepository.AddAsync(user);
       
        user.UserLoginSecurity = new UserLoginSecurity();
        user.UserRoles = userroleList;
        user.UserResendOtpSecurity = new UserResendOtpSecurity();
        await _userRepository.UpdateAsync(user);
        await CreateStaffWhenRolesIncludeStaff(user, roles, organizationName?.Trim());
        scope.Complete();

        return  DataResponse<CreateUserResponse>.Ok(_mapper.Map<CreateUserResponse>(user));
    }


    private async Task CreateStaffWhenRolesIncludeStaff(User user,string?[]? roles, string? organizationName)
    {
        if (roles.Contains(Roles.STAFF))
        {
         if (organizationName.IsNullOrEmpty()) throw new BusinessException("eger usere STAFF rolu vermey isdyirsizse aid oldugu arganizeşini daxil edin");
            await _httpService.PostAsync<ErrorResponse>("https://adminapi20240708182629.azurewebsites.net/api/Staffs/create", new
            {
                fullname = user.FullName,
                username = user.Email,
                organizationName
            },true);
        }
    }


    public async Task<IResponse> Delete(DeleteUserRequest deleteUserRequest)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        if (!HashingHelper.VerifyPasswordHash(deleteUserRequest.Password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("şifrə yalnişdir");
        await _userRepository.DeleteAsync(user);
        return  Response.Ok("hesabiniz uğurla silindi");
    }



    public async Task<IDataResponse<GetUserResponse>> GetByEmail(string email)
    {
        var user = await IfUserNotExistsThrow(email);
        return   DataResponse<GetUserResponse>.Ok(_mapper.Map<GetUserResponse>(user));
    }

    public async Task<IDataResponse<PaginingGetListUserResponse>> GetList(PageRequest pageRequest)
    {
        IPaginate<User>  pageList = await _userRepository.GetListAsync(size: pageRequest.PageSize, index: pageRequest.Page);
        return  DataResponse<PaginingGetListUserResponse>.Ok(_mapper.Map<PaginingGetListUserResponse>(pageList));
    }



    public async Task<IResponse> UpdatePassword(UpdateUserPasswordRequest updateUserPasswordRequest)
    {


        var user = await IfUserNotExistsThrow(_currentUser);
        var passwordIsTrust = HashingHelper.VerifyPasswordHash(updateUserPasswordRequest.CurrentPassword
            , user.PasswordHash, user.PasswordSalt);
        if (!passwordIsTrust)  throw new BusinessException("şifrə yalnişdir");

        HashingHelper.CreatePasswordHash(updateUserPasswordRequest.Password,
            out byte[]  newPasswordHash,out byte[]  newPasswordSalt);
        user.PasswordHash = newPasswordHash;
        user.PasswordSalt = newPasswordSalt;
        await _userRepository.UpdateAsync(user);

        return  Response.Ok("şifrə uğurla yeniləndi");

    }

    public async Task<IResponse> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        user.FullName = updateNameAndSurname.FullName;
        await _userRepository.UpdateAsync(user);
        return  Response.Ok("məlumatlar uğurla yeniləndi");
    }


    public async  Task<IDataResponse<GetUserResponse>> Get()
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        var userDetail = _mapper.Map<GetUserResponse>(user);
        return  DataResponse<GetUserResponse>.Ok(userDetail);
    }

    public async Task<IDataResponse<GetPermissionsUserResponse>> GetForWeb()
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        var userDetail = _mapper.Map<GetPermissionsUserResponse>(user);
        userDetail.Permissions = _jwtService.GetRoles()?.ToArray();
        userDetail.OrganizationName = _jwtService.GetOrganizationName();
        return DataResponse<GetPermissionsUserResponse>.Ok(userDetail);
    }



    public async Task<IResponse> AddRole(UserRoleRequest userRole)
    {
        var user = await IfUserNotExistsThrow(userRole.Email);
        var role = await IfRoleNotExistsThrow(userRole.RoleName);

        user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
        await _userRepository.UpdateAsync(user);
        return Response.Ok();


    }

    public async Task<IResponse> DeleteRole(UserRoleRequest userRole)
    {
        var user = await IfUserNotExistsThrow(userRole.Email);
        var role = await IfRoleNotExistsThrow(userRole.RoleName);
        await _userRoleRepository.DeleteAsync(new UserRole { UserId = user.Id, RoleId = role.Id });
        return Response.Ok();
    }


    public async Task<IResponse> DeleteRoleRange(UserEmailRequest userEmail)
    {
        var user = await IfUserNotExistsThrow(userEmail.Email);
        var datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id);
        if (datas.Items.Count == 0) throw new BusinessException(Messages.RoleDoesNotExists);
        await _userRoleRepository.DeleteAsync(datas.Items);
        return Response.Ok();

    }

    public async Task<IDataResponse<IList<ListRoleResponse>>> GetRoleList(UserEmailRequest userEmail)
    {
        var user = await IfUserNotExistsThrow(userEmail.Email);

        var datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
        if (datas.Items.Count == 0) throw new BusinessException(Messages.RoleDoesNotExists);
        return DataResponse<IList<ListRoleResponse>>.Ok(_mapper.Map<IList<ListRoleResponse>>(datas.Items));
    }

    public async Task<IResponse> AddRoleRange(AddRolesToUserRequest userRoles)
    {
        var user  = await IfUserNotExistsThrow(userRoles.Email);
        var userRoleList = new List<UserRole>();
        foreach (var role in userRoles.RoleRequests) 
        {
           var correctRole =  await IfRoleNotExistsThrow(role.Name);
           userRoleList.Add(new UserRole(user.Id, correctRole.Id));
        }
        await _userRoleRepository.AddAsync(userRoleList);
        return Response.Ok();
    }



    private async Task<User> IfUserNotExistsThrow(string  email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email) 
                   ?? throw new BusinessException("bu emaile uygun  isdifadeci yoxdur..");
        return user;

    }
    private async Task<Role> IfRoleNotExistsThrow(string roleName)
    {
        var role = await _roleRepository.GetAsync(c => c.Name == roleName) ?? throw new BusinessException("role not found");
        return role;

    }

    private async Task EmailIsUnique( string email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email,hasQueryFilterIgnore:true);
        if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
    }

   
}
