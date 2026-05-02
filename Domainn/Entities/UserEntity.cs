namespace Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBanned { get; set; }
        public bool IsMuted { get; set; }
    }
}
