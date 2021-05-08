using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class RecipeResponse : EntityResponse
    {
        [Required]
        [StringLength(256)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [Range(1, Int16.MaxValue)]
        [JsonPropertyName("servings")]
        public int Servings { get; set; }

        [JsonPropertyName("preparation")]
        public PreparationResponse Preparation { get; set; }

        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientResponse> Ingredients { get; set; }

        [JsonPropertyName("user")]
        public UserResponse User { get; set; }

        [Required]
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResponse NutritionFacts { get; set; }
    }
}