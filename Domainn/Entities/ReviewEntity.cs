namespace Domain.Entities
{
    public class ReviewEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionEntity InstitutionEntity { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ReviewScoreEntity> ReviewScore { get; set; }
        public bool IsBanned { get; set; }
    }
}
