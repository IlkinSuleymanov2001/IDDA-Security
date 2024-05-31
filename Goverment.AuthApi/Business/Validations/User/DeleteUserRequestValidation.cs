using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;

namespace Goverment.AuthApi.Business.Validations.User
{
	public class DeleteUserRequestValidation: AbstractValidator<DeleteUserRequest>
	{

        public DeleteUserRequestValidation()
        {
            RuleFor(c=>c.Id).NotNull().NotEmpty();

        }
    }
}
