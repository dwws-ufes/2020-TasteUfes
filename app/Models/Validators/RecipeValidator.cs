using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(r => r.Servings)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(r => r.Preparation)
                .SetValidator(new PreparationValidator());

            RuleForEach(r => r.Ingredients)
                .SetValidator(new IngredientValidator());

            RuleFor(r => r.UserId)
                .NotEmpty();
        }
    }
}