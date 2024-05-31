namespace Goverment.AuthApi.Business.Dtos.Request.UserRole
{
	public class AddRolesToUserRequest
	{
		public int UserId { get; set; }
		public ICollection<int> RolesId {  get; set; }

        public AddRolesToUserRequest()
        {
            RolesId = new HashSet<int>();
        }
    }
}
