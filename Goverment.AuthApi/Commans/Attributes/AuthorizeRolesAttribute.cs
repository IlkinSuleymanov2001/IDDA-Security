using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

namespace Goverment.AuthApi.Commans.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
        Roles = string.Join(",", roles);
    }
}
