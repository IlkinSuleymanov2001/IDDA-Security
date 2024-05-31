namespace Goverment.AuthApi.Business.Dtos.Response.User
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public bool IsVerify { get; set; }

    }
}
