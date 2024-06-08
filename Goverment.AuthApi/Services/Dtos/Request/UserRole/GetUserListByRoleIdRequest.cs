using Core.Application.Requests;

namespace Goverment.AuthApi.Business.Dtos.Request.UserRole
{
	public class GetUserListByRoleIdRequest
	{
        public int RoleId { get; set; }
        public virtual PageRequest PageRequest { get; set; }

    }
}
