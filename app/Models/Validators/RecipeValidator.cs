using FluentValidation;
using Microsoft.Extensions.Localization;

namespace TasteUfes.Models.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator(IStringLocalizer<SharedResources> localizer)
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
                .SetValidator(new IngredientValidator(localizer));

            RuleFor(r => r.UserId)
                .NotEmpty();
        }
    }
}