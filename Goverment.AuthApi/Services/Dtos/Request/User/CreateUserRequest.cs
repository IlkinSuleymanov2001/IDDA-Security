namespace Goverment.AuthApi.Business.Dtos.Request.User
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}
