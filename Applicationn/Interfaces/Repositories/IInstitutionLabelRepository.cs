using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IInstitutionLabelRepository
    {
        Task AddAsync(InstitutionLabelEntity entity);
        Task<List<InstitutionLabelEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid institutionId);
    }
}
