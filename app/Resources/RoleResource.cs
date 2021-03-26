using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class RoleResource : EntityResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}