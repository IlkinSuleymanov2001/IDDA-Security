using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Commans.Constants;

namespace Goverment.AuthApi.Commans.Validations.Auth
{
    public class UserLoginRequestValidation : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidation()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage(Messages.UserNameAndPasswordError);
            RuleFor(c => c.Password).NotEmpty().WithMessage(Messages.UserNameAndPasswordError);
        }
    }
}
