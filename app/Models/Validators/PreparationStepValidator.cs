using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class PreparationStepValidator : AbstractValidator<PreparationStep>
    {
        public PreparationStepValidator()
        {
            RuleFor(r => r.Step)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(r => r.Description)
                .NotEmpty()
                .MaximumLength(2048);
        }
    }
}