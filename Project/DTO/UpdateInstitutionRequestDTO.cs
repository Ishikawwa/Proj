namespace Project.DTO
{
    public class CreateUpdateInstitutionRequestDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateInstitutionRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
}