using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Volvo.CrudTruck.Application.Models;

namespace Volvo.CrudTruck.Application.Validators
{
    public class TruckValidator : AbstractValidator<TruckModel.Request>
    {
        public TruckValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty()
                .NotNull()
                .WithMessage("Preencha um modelo de caminhão válido");

            RuleFor(x => x.Model)
                .Must(c => c.Contains("FH") || c.Contains("FM"))
                .WithMessage("Os modelos permitidos são 'FM' ou 'FH'");

            RuleFor(x => x.ModelYear)
                .NotEmpty()
                .NotNull()
                .WithMessage("Preencha um ano de modelo de caminhão válido");

            RuleFor(x => x.ModelYear)
                .Must(c => c >= DateTime.Now.Year && c < DateTime.Now.Year + 2)
                .WithMessage("Somente ano atual ou subsequente");
        }
    }
}
