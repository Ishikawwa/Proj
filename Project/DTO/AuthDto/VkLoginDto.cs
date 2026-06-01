namespace Project.DTO.AuthDto
{
    public class VkLoginDto
    {
        public string Code { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string CodeVerifier { get; set; } = string.Empty;
    }
}