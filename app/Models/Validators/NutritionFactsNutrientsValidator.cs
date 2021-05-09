using FluentValidation;
using Microsoft.Extensions.Localization;

namespace TasteUfes.Models.Validators
{
    public class NutritionFactsNutrientsValidator : AbstractValidator<NutritionFactsNutrients>
    {
        public NutritionFactsNutrientsValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(n => n.AmountPerServing)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["NutritionFactsNutrients.AmountPerServing.GreaterThanOrEqual", 0]);

            RuleFor(n => n.AmountPerServingUnit)
                .NotEmpty()
                .WithMessage(localizer["NutritionFactsNutrients.AmountPerServingUnit.Required"])
                .IsInEnum()
                .WithMessage(localizer["NutritionFactsNutrients.AmountPerServingUnit.InvalidUnitOfMeasure"])
                .Equal(Measures.g)
                .WithMessage(localizer["NutritionFactsNutrients.AmountPerServingUnit.MustBeInGram"]);

            RuleFor(n => n.NutrientId)
                .NotEmpty()
                .WithMessage(localizer["NutritionFactsNutrients.NutrientId.Required"]);
        }
    }
}