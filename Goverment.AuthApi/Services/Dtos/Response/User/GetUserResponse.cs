namespace Goverment.AuthApi.Business.Dtos.Response.User
{
    public record GetUserResponse
    {
        public required string Email { get; set; }
        public required string  FullNameOrOrganizationName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

    }
}
