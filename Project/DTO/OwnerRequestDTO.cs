namespace Project.DTO
{
    public class CreateOwnerRequestDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }

    public class OwnerRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
}