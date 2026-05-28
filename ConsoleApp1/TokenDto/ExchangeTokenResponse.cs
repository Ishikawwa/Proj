using System.Text.Json.Serialization;

internal class ExchangeTokenResponse
{
    [JsonPropertyName("response")]
    public ExchangeTokenData? Response { get; set; }

    [JsonPropertyName("error")]
    public VkApiError? Error { get; set; }
}