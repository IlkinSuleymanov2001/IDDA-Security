using Goverment.AuthApi.Business.Dtos.Request.Role;

namespace Goverment.AuthApi.Services.Dtos.Request.Role
{
    public class AddRolesToUserRequest
    {
        public string Email { get; set; }
        public ICollection<RoleRequest> RoleRequests { get; set; }

        public AddRolesToUserRequest()
        {
            RoleRequests = new HashSet<RoleRequest>();
        }
    }
}
