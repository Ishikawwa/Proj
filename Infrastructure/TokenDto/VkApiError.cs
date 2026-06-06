using System.Text.Json.Serialization;

internal class VkApiError
{
    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("error_msg")]
    public string ErrorMsg { get; set; } = string.Empty;
}