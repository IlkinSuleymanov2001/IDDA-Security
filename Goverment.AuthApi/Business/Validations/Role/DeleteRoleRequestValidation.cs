using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;

namespace Goverment.AuthApi.Business.Validations.Role
{
	public class DeleteRoleRequestValidation :AbstractValidator<DeleteRoleRequest>
	{

        public DeleteRoleRequestValidation()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();
        }
    }
}
