using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class RecipeResource : EntityResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("servings")]
        public int Servings { get; set; }

        [JsonPropertyName("preparation")]
        public PreparationResource Preparation { get; set; }

        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientResource> Ingredients { get; set; }

        [JsonPropertyName("user")]
        public UserResource User { get; set; }

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResource NutritionFacts { get; set; }
    }
}