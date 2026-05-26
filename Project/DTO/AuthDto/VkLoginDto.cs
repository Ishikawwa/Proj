namespace Project.DTO.AuthDto
{
    public class VkLoginDto
    {
        public string SilentToken { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
    }
}