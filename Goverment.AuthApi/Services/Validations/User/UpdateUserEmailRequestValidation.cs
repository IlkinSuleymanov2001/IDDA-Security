using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Services.Constants;

namespace Goverment.AuthApi.Business.Validations.User
{
	public class UpdateUserEmailRequestValidation: AbstractValidator<UserEmailRequest>
	{

        public UpdateUserEmailRequestValidation()
        {
			RuleFor(c => c.Email).NotEmpty().WithMessage("Email bos kecile bilmez");
			RuleFor(c => c.Email).EmailAddress().WithMessage(Messages.UserNotExists);

		}
	}
}
