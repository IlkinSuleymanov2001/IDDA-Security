using Microsoft.AspNetCore.Authorization;

namespace Goverment.AuthApi.Controllers.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
        Roles = string.Join(",", roles);
    }
}
