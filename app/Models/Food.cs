using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Food : Entity
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [InverseProperty("Food")]
        public IEnumerable<Ingredient> Ingredients { get; set; }

        [ForeignKey("NutritionFactsId")]
        public NutritionFacts NutritionFacts { get; set; }
        public Guid? NutritionFactsId
        {
            get
            {
                return (this._nutritionFactsId == Guid.Empty) ? null : this._nutritionFactsId;
            }

            set
            {
                this._nutritionFactsId = (value == Guid.Empty) ? null : value;
            }
        }

        [NotMapped]
        private Guid? _nutritionFactsId;
    }
}