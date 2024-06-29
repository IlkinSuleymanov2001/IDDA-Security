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
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Constants;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Goverment.AuthApi.Services.Filters.Transaction;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
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

    [Transaction]
    public async Task<IDataResponse<CreateUserResponse>> Create(CreateUserRequest createUserRequest)
    {
        await  EmailIsUnique(createUserRequest.Email);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(createUserRequest.Password, out passwordHash, out passwordSalt);

        User user = new User
        {
            Email = createUserRequest.Email,
            FullName = createUserRequest.FullName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsVerify = true
            
        };

        UserRole userRole = new UserRole { User = user, RoleId = _Role.Id };
        UserLoginSecurity userLoginSecurity = new UserLoginSecurity { User = user, LoginRetryCount = 0 };

        await _userRepository.AddAsync(user);
        await _userRoleRepository.AddAsync(userRole);
        await _loginSecurityRepository.AddAsync(userLoginSecurity);



        return new DataResponse<CreateUserResponse>(_mapper.Map<CreateUserResponse>(user));
    }


    public async Task<IResponse> Delete(DeleteUserRequest deleteUserRequest)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        if (!HashingHelper.VerifyPasswordHash(deleteUserRequest.Password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("password duzgun deyil yeniden cehd edin");
        await _userRepository.DeleteAsync(user);
        return new Response();
    }



    public async Task<IDataResponse<GetUserResponse>> GetByEmail(string email)
    {
        var user = await IfUserNotExistsThrow(email);
        return  new DataResponse<GetUserResponse>(_mapper.Map<GetUserResponse>(user));
    }

    public async Task<IDataResponse<PaginingGetListUserResponse>> GetList(PageRequest pageRequest = null)
    {
        IPaginate<User> pageList;
        if (pageRequest == null)
            pageList = await _userRepository.GetListAsync();

        pageList = await _userRepository.GetListAsync(size: pageRequest.PageSize, index: pageRequest.Page);
        return new DataResponse<PaginingGetListUserResponse>(_mapper.Map<PaginingGetListUserResponse>(pageList));
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

        return new Response();
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

        await _userRepository.SaveChangesAsync();
        return new Response();


    }

    public async Task<IResponse> DeleteRole(UserRoleRequest userrole)
    {
        var user = await IfUserNotExistsThrow(userrole.Email);
        var role = await IfRoleNotExistsThrow(userrole.RoleName);
        await _userRoleRepository.DeleteAsync(new UserRole { UserId = user.Id, RoleId = role.Id });
        return new Response();
    }

    public async Task<IResponse> DeleteRoleRange(UserEmailRequest userEmail)
    {
        var user = await IfUserNotExistsThrow(userEmail.Email);
        var datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id);
        if (datas.Items.Count == 0) throw new BusinessException(Messages.RoleDoesNotExists);
        _userRoleRepository.CustomQuery().RemoveRange(datas.Items);
        await _userRoleRepository.SaveChangesAsync();
        return new Response();

    }

    public async Task<IDataResponse<IList<ListRoleResponse>>> GetRoleList(UserEmailRequest userEmail)
    {
        var user = await IfUserNotExistsThrow(userEmail.Email);

        IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
        if (datas.Items.Count == 0) throw new BusinessException(Messages.RoleDoesNotExists);
        return new DataResponse<IList<ListRoleResponse>>(_mapper.Map<IList<ListRoleResponse>>(datas.Items));
    }

    public async Task<IResponse> AddRoleRange(AddRolesToUserRequest @event)
    {
        var user  = await IfUserNotExistsThrow(@event.Email);
        var roles = new List<Role>();
        foreach (var role in @event.RoleRequests) 
            roles.Add(await IfRoleNotExistsThrow(role.Name));


        foreach (var role in roles)
            await _userRoleRepository.CustomQuery().AddAsync(new UserRole(user.Id, role.Id));


        await _userRoleRepository.SaveChangesAsync();
        return new Response();
    }



    private async Task<User> IfUserNotExistsThrow(string  email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email);
        if (user is null) throw new BusinessException("bu emaile uygun  isdifadeci yoxdur..");
        return user;

    }
    private async Task<Role> IfRoleNotExistsThrow(string roleName)
    {
        var role = await _roleRepository.GetAsync(c => c.Name == roleName.ToUpper());
        if (role is null) throw new BusinessException("role not found");
        return role;

    }

    private async Task EmailIsUnique( string email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email.ToLower());
        if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
    }

   
}
