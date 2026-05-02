using Domain.Enums;

namespace Project.DTO.InstitutionLabelDto
{
    public class InstitutionLabelToCreateDto
    {
        public Guid InstitutionId { get; set; }
        public LabelTypeEnum Label { get; set; }
    }
}
