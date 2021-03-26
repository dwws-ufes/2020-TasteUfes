using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class FoodValidator : AbstractValidator<Food>
    {
        public FoodValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty();
        }
    }
}