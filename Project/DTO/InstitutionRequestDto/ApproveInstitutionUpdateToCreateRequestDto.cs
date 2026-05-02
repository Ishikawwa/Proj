namespace Project.DTO.InstitutionRequestDto
{
    public class ApproveInstitutionUpdateToCreateRequestDto // ---> Было так CreateUpdateInstitutionRequestDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
}
