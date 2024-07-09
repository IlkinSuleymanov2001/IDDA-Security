namespace Goverment.AuthApi.Services.Dtos.Request.Staff
{
    public class CreateStaffRequest
    {
        public string? fullname { get; set; }
        public string username { get; set; }
        public string? organizationName { get; set; }
    }
}
