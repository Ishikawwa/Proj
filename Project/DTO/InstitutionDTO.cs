using Domain.Enums;
using System.Reflection.Emit;

namespace Project.DTO.Institution
{
    public class CreateInstitutionDto
    {
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public class UpdateInstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public class AssignOwnerDto
    {
        public Guid InstitutionId { get; set; }
        public Guid OwnerId { get; set; }
    }

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
    public class CreateWorkingHoursDto
    {
        public Guid InstitutionId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
    public class WorkingHoursDto
    {
        public Guid InstitutionId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }

    public class CreateInstitutionLabelDto
    {
        public Guid InstitutionId { get; set; }
        public Label Label { get; set; }
    }
    public class InstitutionLabelDto
    {
        public Guid InstitutionId { get; set; }
        public Label Label { get; set; }
    }
}