using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IVisitingRepository
    {
        Task AddAsync(VisitingEntity entity);
        Task<List<VisitingEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid id);
    }
}
