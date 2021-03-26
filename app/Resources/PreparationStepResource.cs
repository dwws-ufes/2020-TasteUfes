using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class PreparationStepResource : EntityResource
    {
        [JsonPropertyName("step")]
        public int Step { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("preparation_id")]
        public Guid PreparationId { get; set; }
    }
}