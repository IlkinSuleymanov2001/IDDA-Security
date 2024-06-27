using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Constants;

namespace Goverment.AuthApi.Business.Validations.Auth
{
    public class ResetUserPasswordRequestValidation: AbstractValidator<ResetUserPasswordRequest>
    {


        public ResetUserPasswordRequestValidation()
        {
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password cannot be empty.");
            RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Her iki  Password eyni olmalidir.. ");
            RuleFor(c => c.Password).MinimumLength(8).WithMessage(Messages.PasswordLengthvalidationError);
            RuleFor(c => c.Password).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
            .WithMessage(Messages.PassordFromatValidationError);

            
            RuleFor(c => c.ConfirmPassword).MinimumLength(8).WithMessage(Messages.PasswordLengthvalidationError);
            RuleFor(c => c.ConfirmPassword).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
           .WithMessage(Messages.PassordFromatValidationError);

        }
    }
}
