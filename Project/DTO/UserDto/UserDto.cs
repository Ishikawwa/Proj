namespace Project.DTO.UserDto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
