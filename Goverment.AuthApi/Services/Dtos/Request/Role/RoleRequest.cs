namespace Goverment.AuthApi.Business.Dtos.Request.Role
{
	public class RoleRequest(string name)
    {
        public string Name
        {
            get => name;
            set => name = value.ToUpper();
        }
    }
}
