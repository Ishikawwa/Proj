using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IInstitutionLabelRepository
    {
        Task AddAsync(InstitutionLabelEntity entity);
        Task<List<InstitutionLabelEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid institutionId);
    }
}
