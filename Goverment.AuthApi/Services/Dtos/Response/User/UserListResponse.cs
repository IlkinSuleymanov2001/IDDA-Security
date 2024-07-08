namespace Goverment.AuthApi.Business.Dtos.Response.User
{
	public class UserListResponse
	{
		public string FullName { get; set; }
		public string Email { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
