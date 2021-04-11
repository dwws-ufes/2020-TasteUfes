using System;
using System.Linq;
using FluentValidation;
using TasteUfes.Data.Interfaces;

namespace TasteUfes.Models.Validators
{
    public class NutrientValidator : AbstractValidator<Nutrient>
    {
        public NutrientValidator()
        {

        }
    }
}