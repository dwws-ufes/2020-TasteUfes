using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        private static readonly Measures[] MASS = new[] { Measures.MG, Measures.G, Measures.KG, Measures.UN };
        private static readonly Measures[] VOLUME = new[] { Measures.L, Measures.ML, Measures.UN };

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

            RuleSet("MassVolumeConflict", () =>
            {
                RuleFor(i => i.Food)
                    .NotEmpty()
                    .SetValidator(new FoodValidator());

                When(i => MASS.Contains(i.Food.NutritionFacts.ServingSizeUnit), () =>
                {
                    RuleFor(i => i.QuantityUnit)
                        .Must(u => MASS.Contains(u));
                });

                When(i => VOLUME.Contains(i.Food.NutritionFacts.ServingSizeUnit), () =>
                {
                    RuleFor(i => i.QuantityUnit)
                        .Must(u => VOLUME.Contains(u));
                });
            });
        }
    }
}
