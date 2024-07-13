using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Extensions;
using Goverment.AuthApi.Commans.AOP.Intercept;

namespace Goverment.AuthApi.Commans.AOP.Security
{
    public class SecuredOperation(string roles, IHttpContextAccessor httpContextAccessor) : MethodInterception
    {
        private readonly string[] _roles = roles.Split(',');

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = httpContextAccessor?.HttpContext?.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims != null && roleClaims.Contains(role))
                    return;
            }
            throw new AuthorizationException();
        }
    }

}
