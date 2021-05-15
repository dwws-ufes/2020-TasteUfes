using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class RoleResponse : EntityResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}