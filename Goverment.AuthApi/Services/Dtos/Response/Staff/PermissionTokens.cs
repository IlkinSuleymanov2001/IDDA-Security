using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Services.Dtos.Response.Staff
{
    public class PermissionTokens : Tokens
    {
        public required string?[] Permissons { get; set; }
        //public StaffResponse? User { get; set; }
         
    }

}
