namespace Goverment.AuthApi.Business.Dtos.Response.User
{
	public class ListUserResponse
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public bool IsVerify { get; set; }
	}
}
