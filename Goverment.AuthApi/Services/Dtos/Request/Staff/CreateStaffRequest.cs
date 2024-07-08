namespace Goverment.AuthApi.Services.Dtos.Request.Staff
{
    public class CreateStaffRequest
    {
        public string? Fullname { get; set; }
        public string Username { get; set; }
        public string? OrganizationName { get; set; }
    }
}
