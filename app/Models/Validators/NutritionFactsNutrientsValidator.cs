using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class NutritionFactsNutrientsValidator : AbstractValidator<NutritionFactsNutrients>
    {
        public NutritionFactsNutrientsValidator()
        {
            RuleFor(n => n.AmountPerServing)
                .GreaterThanOrEqualTo(0);

            RuleFor(n => n.AmountPerServingUnit)
                .NotEmpty()
                .IsInEnum()
                .Equal(Measures.g);

            RuleFor(n => n.NutrientId)
                .NotEmpty();
        }
    }
}