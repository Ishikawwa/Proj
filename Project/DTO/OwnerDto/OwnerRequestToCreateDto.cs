namespace Project.DTO.OwnerDto
{
    public class OwnerRequestToCreateDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
}
