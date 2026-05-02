namespace Project.DTO.ReportDto
{
    public class CreateSpamReportDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }
}
