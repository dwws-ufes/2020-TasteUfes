using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class PreparationValidator : AbstractValidator<Preparation>
    {
        public PreparationValidator()
        {
            RuleFor(p => p.PreparationTime)
                .GreaterThan(TimeSpan.Zero)
                .NotEmpty();

            RuleForEach(p => p.Steps)
                .SetValidator(new PreparationStepValidator());
        }
    }
}