﻿using FluentValidation;
using Goverment.AuthApi.Business.Dtos.Request.Role;

namespace Goverment.AuthApi.Commans.Validations.Role
{
    public class CreateRoleRequestValidation : AbstractValidator<RoleRequest>
    {
        public CreateRoleRequestValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Role adi bos ola bilmez ").
                MinimumLength(3).WithMessage("minimum lengh must be 3");
        }
    }
}
