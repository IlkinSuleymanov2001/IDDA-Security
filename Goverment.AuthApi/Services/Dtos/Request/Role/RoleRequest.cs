namespace Goverment.AuthApi.Business.Dtos.Request.Role
{
	public class RoleRequest
	{
        private string? name;
        public string?  Name {
            get 
            {
                    return name;
            }
            set
            {
                 name = value?.ToUpper();
            }
        }
    }
}
