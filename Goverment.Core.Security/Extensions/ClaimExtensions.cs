﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimExtensions
{
 /*   public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
    }
*/
    public static void AddNameIdentifier(this ICollection<Claim> claims, string username)
    {
        claims.Add(new Claim("sub", username));
    }

    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim("authorities", role)));
    }
}