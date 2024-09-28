using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Commans.Constants;

namespace Goverment.AuthApi.Commans.Validations.User
{
    public class UpdateUserEmailRequestValidation : AbstractValidator<UserEmailRequest>
    {

        public UpdateUserEmailRequestValidation()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email bos kecile bilmez");
            RuleFor(c => c.Email).EmailAddress().WithMessage(Messages.EmailAddressNotExists);

        }
    }
}
