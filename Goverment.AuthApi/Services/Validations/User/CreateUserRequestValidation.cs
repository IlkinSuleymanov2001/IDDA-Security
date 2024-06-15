using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.User;

namespace Goverment.AuthApi.Business.Validations.User
{
	public class CreateUserRequestValidation :AbstractValidator<CreateUserRequest>
	{

        public CreateUserRequestValidation()
        {
			
            RuleFor(c=>c.Email).NotEmpty().EmailAddress().WithMessage("Email Address duzgun deyil  ");
			RuleFor(c => c.FirstName).NotEmpty().WithMessage("bos kecile bilmez");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password cannot be empty.");
			RuleFor(c => c.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
			RuleFor(c => c.Password).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
			.WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, one symbol, and be at least 8 characters long.");

			RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty.");
			RuleFor(c => c.ConfirmPassword).MinimumLength(8).WithMessage("ConfirmPassword must be at least 8 characters long.");
			RuleFor(c => c.ConfirmPassword).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$")
		   .WithMessage("ConfirmPassword must contain at least one uppercase letter, one lowercase letter, one digit, one symbol, and be at least 8 characters long.");

			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Confirm Password duzgun deyil ");
		}
    }
}
