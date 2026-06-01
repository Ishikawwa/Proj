namespace Application.Interfaces
{
    public class VkUserInfo
    {
        public string VkId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? Email { get; set; }
    }

    public interface IVkAuthService
    {
        Task<VkUserInfo?> ExchangeCodeAsync(string code, string deviceId, string codeVerifier);
    }
}