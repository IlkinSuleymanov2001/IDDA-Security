namespace Goverment.AuthApi.Business.Dtos.Request.User
{
    public class CreateUserRequest(string password, string confirmPassword)
    {
        private  string _email=string.Empty;

        public string Email { get => _email;
            set => _email = value.Trim().ToLower();
        }

        private string _fullname=string.Empty; 
        public string FullName { get => _fullname;
            set => _fullname = value.Trim();
        }
        public string Password { get; set; } = password;
        public string ConfirmPassword { get; set; } = confirmPassword;


    }
}
