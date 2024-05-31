using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;

namespace Goverment.AuthApi.Business.Validations.Role
{
	public class UpdateRoleRequestValidation:AbstractValidator<UpdateRoleRequest>
	{
        public UpdateRoleRequestValidation()
        {
            RuleFor(c=>c.Id).NotNull().NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MinimumLength(3).WithMessage("minimum length must be 3");
        }
    }
}
