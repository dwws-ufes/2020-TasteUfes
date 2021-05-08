using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class AnonymousRecipeResponse
    {
        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientResponse> Ingredients { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResponse NutritionFacts { get; set; }
    }
}