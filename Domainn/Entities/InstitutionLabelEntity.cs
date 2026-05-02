using Domain.Enums;

namespace Domain.Entities
{
    public class InstitutionLabelEntity
    {
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
        public LabelTypeEnum Label { get; set; }
    }
}
