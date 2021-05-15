using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public class AnonymousRecipeRequest
    {
        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientRequest> Ingredients { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsRequest NutritionFacts { get; set; }
    }
}