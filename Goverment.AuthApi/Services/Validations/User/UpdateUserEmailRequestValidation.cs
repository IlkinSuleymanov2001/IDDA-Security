using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;

namespace Goverment.AuthApi.Business.Validations.User
{
	public class UpdateUserEmailRequestValidation: AbstractValidator<UpdateUserEmailRequest>
	{

        public UpdateUserEmailRequestValidation()
        {
			RuleFor(c => c.Email).NotEmpty().WithMessage("Email bos kecile bilmez");
			RuleFor(c => c.Email).EmailAddress().WithMessage("Email Address duzgun deyil");

		}
	}
}
