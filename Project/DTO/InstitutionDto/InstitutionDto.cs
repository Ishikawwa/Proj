using Domain.Enums;

namespace Project.DTO.InstitutionDto
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public float Rating { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
