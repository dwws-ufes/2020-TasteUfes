using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public class PreparationStepRequest : EntityRequest
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