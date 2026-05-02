namespace Project.DTO.InstitutionRequestDto
{
    public class InstitutionRequestToUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
}
