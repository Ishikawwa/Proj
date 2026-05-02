namespace Project.DTO.ReviewDto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
