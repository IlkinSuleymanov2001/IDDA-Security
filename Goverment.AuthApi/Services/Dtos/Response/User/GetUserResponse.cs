namespace Goverment.AuthApi.Business.Dtos.Response.User
{
    public class GetUserResponse
    {
        public string Email { get; set; }
        public string  FullName { get; set; }
        public bool Status{ get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

    }
}
