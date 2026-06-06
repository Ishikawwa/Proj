using System.Text.Json.Serialization;

internal class ExchangeTokenData
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}