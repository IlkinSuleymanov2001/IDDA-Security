using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.Role;

namespace Goverment.AuthApi.Business.Validations.Auth
{
    public class UserLoginRequestValidation: AbstractValidator<UserLoginRequest>
	{
        public UserLoginRequestValidation()
        {
			RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Emailin  formati duzgun deyil");
			RuleFor(c => c.Password).NotEmpty().WithMessage(" Passwordu yazin ");
		}
    }
}
