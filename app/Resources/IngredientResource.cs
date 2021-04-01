using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Resources
{
    public class IngredientResource : EntityResource
    {
        [Required]
        [Range(1, Int16.MaxValue)]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [Required]
        [JsonPropertyName("quantity_unit")]
        public Measures QuantityUnit { get; set; }

        [JsonPropertyName("food")]
        public FoodResource Food { get; set; }

        [Required]
        [JsonPropertyName("food_id")]
        public Guid FoodId { get; set; }
    }
}