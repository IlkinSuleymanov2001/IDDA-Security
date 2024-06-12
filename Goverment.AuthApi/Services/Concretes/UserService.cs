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
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
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
        IHttpContextAccessor httpContextAccessor
       ,IUserRoleRepository userRoleRepository,
        IUserLoginSecurityRepository loginSecurityRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = token;
        _currentUser = _jwtService.GetUserEmail(Helper.GetToken(httpContextAccessor));
        _userRoleRepository = userRoleRepository;
        _loginSecurityRepository = loginSecurityRepository;
        _roleRepository = roleRepository;
    }

    public async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest)
    {
        await  EmailIsUnique(createUserRequest.Email);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(createUserRequest.Password, out passwordHash, out passwordSalt);

        User user = new User
        {
            Email = createUserRequest.Email.ToLower(),
            FirstName = createUserRequest.FirstName,
            LastName = createUserRequest.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsVerify = true
            
        };
        user.UserRoles.Add(new UserRole(user.Id, _Role.Id));
        user.UserLoginSecurity = new UserLoginSecurity { UserId = user.Id, LoginRetryCount = 0 };
        await _userRepository.AddAsync(user);
     

        return _mapper.Map<CreateUserResponse>(user);
    }


    public async Task Delete(DeleteUserRequest deleteUserRequest)
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        if (!HashingHelper.VerifyPasswordHash(deleteUserRequest.Password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("password duzgun deyil yeniden cehd edin");
        await _userRepository.DeleteAsync(user);
    }



    public async Task<GetUserResponse> GetByEmail(string email)
    {
        var user = await IfUserNotExistsThrow(email);
        return _mapper.Map<GetUserResponse>(user);
    }

    public async Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest = null)
    {
        IPaginate<User> pageList;
        if (pageRequest == null)
            pageList = await _userRepository.GetListAsync();

        pageList = await _userRepository.GetListAsync(size: pageRequest.PageSize, index: pageRequest.Page);
        return _mapper.Map<PaginingGetListUserResponse>(pageList);
    }



    public async Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
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

    }

    public async Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname)
    {


        var user = await IfUserNotExistsThrow(_currentUser);
        user.FirstName = updateNameAndSurname.FirstName;
        user.LastName = updateNameAndSurname.LastName;
        await _userRepository.UpdateAsync(user);
    }

    public async  Task<GetUserResponse> Get()
    {
        var user = await IfUserNotExistsThrow(_currentUser);
        return _mapper.Map<GetUserResponse>(user);
    }
    public async Task AddRole(UserRoleRequest @event)
    {
        var user = await IfUserNotExistsThrow(@event.Email);
        var role = await IfRoleNotExistsThrow(@event.RoleName);

        user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });

        await _userRepository.SaveChangesAsync();


    }

    public async Task DeleteRole(UserRoleRequest @event)
    {
        var user = await IfUserNotExistsThrow(@event.Email);
        var role = await IfRoleNotExistsThrow(@event.RoleName);
        await _userRoleRepository.DeleteAsync(new UserRole { UserId = user.Id, RoleId = role.Id });
    }

    public async Task DeleteRoleRange(UserEmailRequest @event)
    {
        var user = await IfUserNotExistsThrow(@event.Email);
        var datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id);
        if (datas.Items.Count == 0) return;
        _userRoleRepository.CustomQuery().RemoveRange(datas.Items);
        await _userRoleRepository.SaveChangesAsync();

    }

    public async Task<IList<ListRoleResponse>> GetRoleList(UserEmailRequest @event)
    {
        var user = await IfUserNotExistsThrow(@event.Email);

        IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
        if (datas.Items.Count == 0) throw new BusinessException("User in Rollari yoxdur");
        return _mapper.Map<IList<ListRoleResponse>>(datas.Items);
    }

    public async Task AddRoleRange(AddRolesToUserRequest @event)
    {
        var user  = await IfUserNotExistsThrow(@event.Email);
        var roles = new List<Role>();
        foreach (var role in @event.RoleRequests) 
            roles.Add(await IfRoleNotExistsThrow(role.Name));


        foreach (var role in roles)
            await _userRoleRepository.CustomQuery().AddAsync(new UserRole(user.Id, role.Id));


        await _userRoleRepository.SaveChangesAsync();
    }

    private async Task<User> IfUserNotExistsThrow(string  email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email.ToLower());
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
