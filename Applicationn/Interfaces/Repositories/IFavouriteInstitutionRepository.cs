using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IFavouriteInstitutionRepository
    {
        Task AddAsync(FavouriteInstitutionEntity entity);
        Task<List<FavouriteInstitutionEntity>> GetByUserIdAsync(Guid userId);
        Task DeleteAsync(Guid id);
        Task<FavouriteInstitutionEntity?> GetByIdAsync(Guid id);
    }
}
