using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class UserLoginResponse
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}