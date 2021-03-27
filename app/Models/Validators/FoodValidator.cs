using FluentValidation;

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