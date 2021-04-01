using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(i => i.Quantity)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(i => i.QuantityUnit)
                .NotEmpty()
                .IsInEnum()
                .NotEqual(Measures.KCAL);

            RuleFor(i => i.FoodId)
                .NotEmpty();
        }
    }
}