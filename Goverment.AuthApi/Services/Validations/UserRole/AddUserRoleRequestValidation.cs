using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;

namespace Goverment.AuthApi.Business.Validations.UserRole
{
	public class AddUserRoleRequestValidation: AbstractValidator<AddUserRoleRequest>
	{
        public AddUserRoleRequestValidation()
        {
            RuleFor(c=>c.UserId).NotNull().NotEmpty();
			RuleFor(c => c.RoleId).NotNull().NotEmpty();
		}
            
	}
}
