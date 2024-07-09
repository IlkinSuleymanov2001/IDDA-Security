namespace Goverment.AuthApi.Business.Dtos.Response.User
{
    public record GetUserResponse
    {
        public required string Email { get; set; }
        public required string  FullName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

    }

    public record GetPermissionsUserResponse : GetUserResponse
    {
        public string?[]? Permissions { get; set; }
        public string?  OrganizationName { get; set; }
    }
}
