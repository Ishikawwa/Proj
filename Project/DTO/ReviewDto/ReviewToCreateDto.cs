namespace Project.DTO.ReviewDto
{
    public class ReviewToCreateDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }
}
