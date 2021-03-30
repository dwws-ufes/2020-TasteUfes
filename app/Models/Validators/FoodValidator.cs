using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class FoodValidator : AbstractValidator<Food>
    {
        public FoodValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty();

            RuleFor(f => f.NutritionFacts)
                .SetValidator(new NutritionFactsValidator());
        }
    }
}