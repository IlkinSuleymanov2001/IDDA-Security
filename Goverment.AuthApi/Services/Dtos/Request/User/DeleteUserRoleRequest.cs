namespace Goverment.AuthApi.Services.Dtos.Request.User
{
    public class DeleteUserRoleRequest
    {
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
