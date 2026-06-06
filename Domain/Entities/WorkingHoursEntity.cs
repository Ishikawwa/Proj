namespace Domain.Entities
{
    public class WorkingHoursEntity
    {
        public Guid Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
    }
}
