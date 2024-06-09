using Core.Application.Requests;

namespace Goverment.AuthApi.Services.Dtos.Request.Role
{
    public class UserListByRoleRequest
    {
        public string RoleName { get; set; }
        public virtual PageRequest PageRequest { get; set; }

    }
}
