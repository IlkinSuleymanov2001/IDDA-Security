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
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Business.Utlilities.Caches;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _jwtService;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserLoginSecurityRepository _loginSecurityRepository;
        private readonly int _currentUserId;


        public UserManager(IUserRepository userRepository, IMapper mapper,
            ITokenHelper token, IHttpContextAccessor httpContextAccessor
           , IUserRoleRepository userRoleRepository, IUserLoginSecurityRepository loginSecurityRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = token;
            _currentUserId = _jwtService.GetUserIdFromToken(Helper.GetToken(httpContextAccessor));
            _userRoleRepository = userRoleRepository;
            _loginSecurityRepository = loginSecurityRepository;
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
            await _userRepository.AddAsync(user);
            await _userRoleRepository.AddAsync(new UserRole{ UserId = user.Id, RoleId = JwtHelper.defaultRoleIdWhenUserCreated });
            await _loginSecurityRepository.AddAsync(new UserLoginSecurity { UserId = user.Id, LoginRetryCount = 0 });

            return _mapper.Map<CreateUserResponse>(user);
        }


        public async Task Delete()
        {
            var deleteUser = await IfUserNotExistsThrow(_currentUserId);
            await _userRepository.DeleteAsync(deleteUser);
        }



        public async Task<GetUserResponse> GetById(int userId)
        {
            var user = await IfUserNotExistsThrow(userId);
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

        public async Task UpdateUserEmail(UpdateUserEmailRequest updateUserRequest)
        {
            var user = await IfUserNotExistsThrow(_currentUserId);
            await EmailIsUniqueWhenUpdateEmail(_currentUserId, updateUserRequest.Email);
            user.Email = updateUserRequest.Email;
            await _userRepository.UpdateAsync(user);

        }

        public async Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
        {


            var user = await IfUserNotExistsThrow(_currentUserId);
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


            var user = await IfUserNotExistsThrow(_currentUserId);
            user.FirstName = updateNameAndSurname.FirstName;
            user.LastName = updateNameAndSurname.LastName;
            await _userRepository.UpdateAsync(user);
        }

        public async  Task<GetUserResponse> Get()
        {
            var user = await IfUserNotExistsThrow(_currentUserId);
            return _mapper.Map<GetUserResponse>(user);
        }

        private async Task<User> IfUserNotExistsThrow(int id)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            if (user == null) throw new BusinessException("Bele bir ID-li isdifadeci yoxdur..");
            return user;

        }
        private async Task EmailIsUniqueWhenUpdateEmail(int userId, string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower() && u.Id != userId);
            if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
        }

        private async Task EmailIsUnique( string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower());
            if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
        }

     
    }
}
