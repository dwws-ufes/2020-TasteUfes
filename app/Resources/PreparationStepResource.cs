using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class PreparationStepResource : EntityResource
    {
        [Required]
        [Range(1, Int16.MaxValue)]
        [JsonPropertyName("step")]
        public int Step { get; set; }

        [Required]
        [StringLength(2048)]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}