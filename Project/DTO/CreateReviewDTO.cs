namespace Project.DTO
{
    public class CreateReviewDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public class UpdateReviewDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ScoreReviewDto
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
    }
}