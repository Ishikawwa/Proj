using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IViewingRepository
    {
        Task AddAsync(ViewingEntity entity);
        Task<List<ViewingEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid id);
    }
}
