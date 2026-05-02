namespace Domain.Entities
{
    public class FavouriteInstitutionEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid InstitutionId { get; set; }
        public InstitutionEntity Institution { get; set; }
    }
}
