namespace Goverment.AuthApi.Business.Dtos.Request
{
	public class UpdateRoleRequest
	{
		private string name;
		private string? newname;
		public string Name { get { return name; } set { name = value.ToUpper(); } }
		public string? NewName { get { return newname; } set { newname = value.ToUpper(); } }

	}
}
