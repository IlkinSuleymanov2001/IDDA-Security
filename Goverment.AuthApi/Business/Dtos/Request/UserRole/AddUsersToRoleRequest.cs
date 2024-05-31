namespace Goverment.AuthApi.Business.Dtos.Request.UserRole
{
	public class AddUsersToRoleRequest
	{
        public int RoleId { get; set; }
		public ICollection<int> UsersId { get; set; }

        public AddUsersToRoleRequest()
        {
            UsersId = new HashSet<int>();
        }
    }
}
