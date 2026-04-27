using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IInstitutionRepository
    {
        Task AddAsync(InstitutionEntity entity);
        Task<InstitutionEntity> GetByIdAsync(Guid id);
        Task<List<InstitutionEntity>> GetAllAsync();
        Task<List<InstitutionEntity>> GetByOwnerIdAsync(Guid ownerId);
        Task UpdateAsync(InstitutionEntity entity);
        Task DeleteAsync(Guid id);
        Task AssignOwnerAsync(Guid institutionId, Guid ownerId);
    }
}
