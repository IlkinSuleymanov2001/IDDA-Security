using System.Security.Claims;
using Core.Security.JWT;

namespace Core.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        List<string>? result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(Config.Roles);
    }

    public static string?  GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(Config.Username)?.FirstOrDefault();
    }
}