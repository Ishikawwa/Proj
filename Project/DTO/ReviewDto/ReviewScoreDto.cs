namespace Project.DTO.ReviewDto
{
    public class ReviewScoreDto
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
    }
}
