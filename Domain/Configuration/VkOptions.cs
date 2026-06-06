public class VkOptions
{
    public static string Section => nameof(VkOptions);
    public string ServiceToken { get; set; }
    public string ClientId { get; set; }
    public string RedirectUri { get; set; }
}