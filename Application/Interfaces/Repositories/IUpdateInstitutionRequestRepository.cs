using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUpdateInstitutionRequestRepository
    {
        Task AddAsync(UpdateInstitutionRequestEntity entity);
        Task<List<UpdateInstitutionRequestEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
