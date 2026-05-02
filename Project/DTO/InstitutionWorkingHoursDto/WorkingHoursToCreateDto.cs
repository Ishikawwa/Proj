namespace Project.DTO.InstitutionWorkingHoursDto
{
    public class WorkingHoursToCreateDto
    {
        public Guid InstitutionId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
