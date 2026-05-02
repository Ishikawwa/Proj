using Domain.Enums;

namespace Project.DTO.InstitutionDto
{
    public class InstitutionToUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }
}
