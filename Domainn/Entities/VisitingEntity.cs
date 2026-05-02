namespace Domain.Entities
{
    public class VisitingEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
    }
}
