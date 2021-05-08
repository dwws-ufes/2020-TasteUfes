using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class RefreshTokenResponse
    {
        [Required]
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [Required]
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}