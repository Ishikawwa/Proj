using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IViewingRepository
    {
        Task AddAsync(ViewingEntity entity);
        Task<List<ViewingEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid id);
    }
}
