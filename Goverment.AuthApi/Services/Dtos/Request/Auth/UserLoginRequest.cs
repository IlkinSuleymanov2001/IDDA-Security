namespace Goverment.AuthApi.Business.Dtos.Request.Auth
{
	public class UserLoginRequest
	{
        private string _email = string.Empty;
        public string Email { get { return _email; } set { _email = value.Trim().ToLower(); } }
        public string Password { get; set; }

    }
}
