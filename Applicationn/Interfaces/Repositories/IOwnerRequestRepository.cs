using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOwnerRequestRepository
    {
        Task AddAsync(OwnerRequestEntity entity);
        Task<List<OwnerRequestEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
