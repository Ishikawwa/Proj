public class JwtOptions
{
    public static string Section => nameof(JwtOptions);
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresInDays { get; set; }
}