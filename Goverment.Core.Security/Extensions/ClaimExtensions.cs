using Core.Security.JWT;
using Goverment.Core.Security.JWT;
using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimExtensions
{

    public static void AddUsername(this ICollection<Claim> claims, string username)=>
        claims.Add(new Claim(Config.Username, username));
    public static void AddFullName(this ICollection<Claim> claims, string fullname)=>
        claims.Add(new Claim("fullName", fullname));

    public static void AddParammetr(this ICollection<Claim> claims,AddtionalParam addtionalParam)=>
        claims.Add(new Claim(addtionalParam.Key, addtionalParam.Value));

    public static void AddRoles(this ICollection<Claim> claims, string?[] roles)=>
        roles.ToList().ForEach(role => claims.Add(new Claim(Config.Roles, role)));
}