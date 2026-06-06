using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IVisitingRepository
    {
        Task AddAsync(VisitingEntity entity);
        Task<List<VisitingEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid id);
    }
}
