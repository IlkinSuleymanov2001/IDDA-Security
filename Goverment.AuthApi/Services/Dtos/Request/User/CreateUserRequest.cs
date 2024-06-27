namespace Goverment.AuthApi.Business.Dtos.Request.User
{
    public class CreateUserRequest
    {
        private  string _email=string.Empty;
        public string Email { get { return _email; }  set { _email = value.Trim().ToLower(); } }

        private string _fullname=string.Empty; 
        public string FullName { get { return _fullname; } set { _fullname = value.Trim(); } }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}
