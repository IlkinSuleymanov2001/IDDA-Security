using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;

namespace Goverment.AuthApi.Commans.Validations.Role
{
    public class UpdateRoleRequestValidation : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidation()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(3);
            RuleFor(c => c.NewName).NotEmpty().MinimumLength(3);
        }
    }
}
