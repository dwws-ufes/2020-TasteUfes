using FluentValidation;
using Microsoft.Extensions.Localization;

namespace TasteUfes.Models.Validators
{
    public class NutritionFactsValidator : AbstractValidator<NutritionFacts>
    {
        public NutritionFactsValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(n => n.ServingSize)
                .NotEmpty()
                .WithMessage(localizer["NutritionFacts.ServingSize.Required"])
                .GreaterThan(0)
                .WithMessage(localizer["NutritionFacts.ServingSize.GreaterThan", 0]);

            RuleFor(n => n.ServingSizeUnit)
                .NotEmpty()
                .WithMessage(localizer["NutritionFacts.ServingSizeUnit.Required"])
                .IsInEnum()
                .WithMessage(localizer["NutritionFacts.ServingSizeUnit.InvalidUnitOfMeasure"]);

            RuleForEach(n => n.NutritionFactsNutrients)
                .SetValidator(new NutritionFactsNutrientsValidator(localizer));
        }
    }
}