using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Commans.Constants;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
namespace Goverment.AuthApi.Business.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _jwtService;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserLoginSecurityRepository _loginSecurityRepository;
    private readonly string  _currentUser;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        ITokenHelper token,
        IUserRoleRepository userRoleRepository,
        IUserLoginSecurityRepository loginSecurityRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = token;
        _currentUser = _jwtService.GetUsername();
        _userRoleRepository = userRoleRepository;
        _loginSecurityRepository = loginSecurityRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IDataResponse<CreateUserResponse>> Create(CreateUserRequest createUserRequest, params string?[] roles)
    {
        await  EmailIsUnique(createUserRequest.Email);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(createUserRequest.Password, out passwordHash, out passwordSalt);

        var userroleList = new List<UserRole>([new UserRole { Role = new(_Role.Id, _Role.Name) }]);
        var user = new User
        {
            Email = createUserRequest.Email,
            FullName = createUserRequest.FullName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsVerify = true,
        };

        if (!roles.IsNullOrEmpty()) 
        {
            var rolesList = await _roleRepository.ListAsync(r => roles.Contains(r.Name));
            if (rolesList.Count()!=roles.Length) throw new BusinessException(Messages.RoleDoesNotExists);
             userroleList.AddRange(rolesList.Select(role => new UserRole { Role = role }));
        }

        await _userRepository.AddAsync(user);

        user.UserLoginSecurity = new UserLoginSecurity();
        user.UserRoles = userroleList;
        user.UserResendOtpSecurity = new UserResendOtpSecurity();

        await _userRepository.UpdateAsync(user);

        return new DataResponse<CreateUserResponse>(_mapper.Map<CreateUserResponse>(user));
    }


    public async Task<IResponse> Delete(DeleteUserRequest deleteUserRequest)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        if (!HashingHelper.VerifyPasswordHash(deleteUserRequest.Password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("password duzgun deyil yeniden cehd edin");
        await _userRepository.DeleteAsync(user);
        return  Response.Ok();
    }



    public async Task<IDataResponse<GetUserResponse>> GetByEmail(string email)
    {
        var user = await IfUserNotExistsThrow(email);
        return   DataResponse<GetUserResponse>.Ok(_mapper.Map<GetUserResponse>(user));
    }

    public async Task<IDataResponse<PaginingGetListUserResponse>> GetList(PageRequest pageRequest = null)
    {
        IPaginate<User> pageList;
        if (pageRequest == null)
            pageList = await _userRepository.GetListAsync();


        pageList = await _userRepository.GetListAsync(size: pageRequest.PageSize, index: pageRequest.Page);
        return  DataResponse<PaginingGetListUserResponse>.Ok(_mapper.Map<PaginingGetListUserResponse>(pageList));
    }



    public async Task<IResponse> UpdatePassword(UpdateUserPasswordRequest updateUserPasswordRequest)
    {


        var user = await IfUserNotExistsThrow(_currentUser);
        var passwordIsTrust = HashingHelper.VerifyPasswordHash(updateUserPasswordRequest.CurrentPassword
            , user.PasswordHash, user.PasswordSalt);
        if (!passwordIsTrust)
            throw new BusinessException("Cari Password duzgun deyil..");

        byte[] newPasswordHash, newPasswordSalt;
        HashingHelper.CreatePasswordHash(updateUserPasswordRequest.Password, out newPasswordHash
            , out newPasswordSalt);

        user.PasswordHash = newPasswordHash;
        user.PasswordSalt = newPasswordSalt;
        await _userRepository.UpdateAsync(user);

        return new Response();

    }

    public async Task<IResponse> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        user.FullName = updateNameAndSurname.FullName;
        await _userRepository.UpdateAsync(user);

        return  Response.Ok();
    }

    public async  Task<IDataResponse<GetUserResponse>> Get()
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        return new DataResponse<GetUserResponse>(_mapper.Map<GetUserResponse>(user));
    }
    public async Task<IResponse> AddRole(UserRoleRequest userrole)
    {
        var user = await IfUserNotExistsThrow(userrole.Email);
        var role = await IfRoleNotExistsThrow(userrole.RoleName);

        user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
        await _userRepository.UpdateAsync(user);
        return Response.Ok();


    }

    public async Task<IResponse> DeleteRole(UserRoleRequest userrole)
    {
        var user = await IfUserNotExistsThrow(userrole.Email);
        var role = await IfRoleNotExistsThrow(userrole.RoleName);
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

        IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
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
        var user = await _userRepository.GetAsync(u => u.Email == email);
        if (user is null) throw new BusinessException("bu emaile uygun  isdifadeci yoxdur..");
        return user;

    }
    private async Task<Role> IfRoleNotExistsThrow(string roleName)
    {
        var role = await _roleRepository.GetAsync(c => c.Name == roleName);
        if (role is null) throw new BusinessException("role not found");
        return role;

    }

    private async Task EmailIsUnique( string email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email,hasQueryFilterIgnore:true);
        if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
    }

   
}
