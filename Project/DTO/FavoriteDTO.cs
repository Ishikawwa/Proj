namespace Project.DTO.Favorite
{
    public class AddInFavouriteListDto
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }

    public class FavouriteInstitutionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
}