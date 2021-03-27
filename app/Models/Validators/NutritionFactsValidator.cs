using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class NutritionFactsValidator : AbstractValidator<NutritionFacts>
    {
        public NutritionFactsValidator()
        {
            RuleFor(n => n.ServingSize)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(n => n.ServingSizeUnit)
                .NotEmpty()
                .IsInEnum();

            RuleFor(n => n.ServingEnergy)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleForEach(n => n.NutritionFactsNutrients)
                .SetValidator(new NutritionFactsNutrientsValidator());
        }
    }
}