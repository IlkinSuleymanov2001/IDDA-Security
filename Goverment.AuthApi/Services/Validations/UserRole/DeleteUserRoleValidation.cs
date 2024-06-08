using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;

namespace Goverment.AuthApi.Business.Validations.UserRole
{
	public class DeleteUserRoleValidation:AbstractValidator<DeleteUserRoleRequest>
	{

        public DeleteUserRoleValidation()
        {
			RuleFor(c => c.UserId).NotNull().NotEmpty();
			RuleFor(c => c.RoleId).NotNull().NotEmpty();
		}
    }
}
