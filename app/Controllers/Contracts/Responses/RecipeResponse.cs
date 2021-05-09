using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class RecipeResponse : EntityResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("servings")]
        public int Servings { get; set; }

        [JsonPropertyName("preparation")]
        public PreparationResponse Preparation { get; set; }

        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientResponse> Ingredients { get; set; }

        [JsonPropertyName("user")]
        public UserResponse User { get; set; }

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResponse NutritionFacts { get; set; }
    }
}