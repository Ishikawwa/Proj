namespace Project.DTO.ReportDto
{
    public class CreateReviewReportDto
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }
}
