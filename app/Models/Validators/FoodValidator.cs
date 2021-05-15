using FluentValidation;
using Microsoft.Extensions.Localization;

namespace TasteUfes.Models.Validators
{
    public class FoodValidator : AbstractValidator<Food>
    {
        public FoodValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(f => f.Name)
                .NotEmpty()
                .WithMessage(localizer["Food.Name.Required"]);

            RuleFor(f => f.NutritionFacts)
                .SetValidator(new NutritionFactsValidator(localizer));
        }
    }
}