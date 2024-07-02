using FluentValidation;
using Goverment.AuthApi.Services.Dtos.Request.User;

namespace Goverment.AuthApi.Commans.Validations.UserRole
{
    public class UserRoleValidation : AbstractValidator<UserRoleRequest>
    {
        public UserRoleValidation()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.RoleName).NotEmpty();
        }

    }
}
