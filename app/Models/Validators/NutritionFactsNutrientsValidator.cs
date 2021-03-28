using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class NutritionFactsNutrientsValidator : AbstractValidator<NutritionFactsNutrients>
    {
        public NutritionFactsNutrientsValidator()
        {
            RuleFor(n => n.AmountPerServing)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(n => n.AmountPerServingUnit)
                .NotEmpty()
                .IsInEnum();

            RuleFor(n => n.NutrientId)
                .NotEmpty();
        }
    }
}