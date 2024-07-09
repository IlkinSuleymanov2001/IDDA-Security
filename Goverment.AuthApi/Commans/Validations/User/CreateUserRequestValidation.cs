using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Commans.Constants;

namespace Goverment.AuthApi.Commans.Validations.User
{
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {

        public CreateUserRequestValidation()
        {

            RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Email Address duzgun deyil");
            RuleFor(c => c.FullName).NotEmpty().WithMessage("bos kecile bilmez");
            RuleFor(c => c.Password).NotEmpty().WithMessage("bos kecile bilmez");
            RuleFor(c => c.Password).MinimumLength(8).WithMessage(Messages.PasswordLengthvalidationError);
            RuleFor(c => c.Password).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
            .WithMessage(Messages.PassordFromatValidationError);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Hər iki şifrə dəqiq eyni olmalıdır");

            RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty.");
            RuleFor(c => c.ConfirmPassword).MinimumLength(8).WithMessage(Messages.PasswordLengthvalidationError);
            RuleFor(c => c.ConfirmPassword).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
           .WithMessage(Messages.PassordFromatValidationError);

        }
    }
}
