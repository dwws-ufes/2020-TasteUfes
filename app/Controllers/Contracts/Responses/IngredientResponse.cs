using System;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class IngredientResponse : EntityResponse
    {
        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("quantity_unit")]
        public Measures QuantityUnit { get; set; }

        [JsonPropertyName("food")]
        public FoodResponse Food { get; set; }

        [JsonPropertyName("food_id")]
        public Guid FoodId { get; set; }
    }
}