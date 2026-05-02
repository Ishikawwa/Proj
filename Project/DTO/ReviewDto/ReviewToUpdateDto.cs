namespace Project.DTO.ReviewDto
{
    public class ReviewToUpdateDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }
}
