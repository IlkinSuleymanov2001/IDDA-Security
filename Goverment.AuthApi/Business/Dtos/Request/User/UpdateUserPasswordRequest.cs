namespace Goverment.AuthApi.Business.Dtos.Request.User
{
	public class UpdateUserPasswordRequest
	{
        public int Id { get; set; }
        public string  CurrentPassword  { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
    }
}
