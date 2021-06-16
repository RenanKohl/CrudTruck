using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Volvo.CrudTruck.Application.Models;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Application.Validators
{
    public class UserValidator : AbstractValidator<UserModel.Request>
    {
        public UserValidator()
        {
            RuleFor(x => x.Login).NotEmpty().NotNull().WithMessage("Login inválido!");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Senha inválida!");
        }
    }
}
