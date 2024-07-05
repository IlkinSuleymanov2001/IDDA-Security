using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Services.Dtos.Response.Staff
{
    public class PermissionTokens : Tokens
    {
        public required string[] Permissons { get; set; }
        public StaffResponse? Staff { get; set; }
         
    }

}
