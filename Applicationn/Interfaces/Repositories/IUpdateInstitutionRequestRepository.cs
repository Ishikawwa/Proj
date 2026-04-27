using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUpdateInstitutionRequestRepository
    {
        Task AddAsync(UpdateInstitutionRequestEntity entity);
        Task<List<UpdateInstitutionRequestEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
