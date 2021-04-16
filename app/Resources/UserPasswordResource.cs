using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class UserPasswordResource
    {
        [Required]
        [StringLength(32, MinimumLength = 6)]
        [JsonPropertyName("old_password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; }
    }
}