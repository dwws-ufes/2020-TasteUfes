using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public class RoleRequest : EntityRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}