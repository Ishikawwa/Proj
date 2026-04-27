namespace Project.DTO
{
    public class CreateVisitingDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
    public class VisitingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
    public class CreateViewingDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
    public class ViewingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
}