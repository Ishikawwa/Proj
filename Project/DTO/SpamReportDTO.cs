namespace Project.DTO
{
    public class CreateReviewReportDto
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }

    public class SpamReportDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }
}