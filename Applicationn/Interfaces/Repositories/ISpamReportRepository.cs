using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISpamReportRepository
    {
        Task AddAsync(SpamReportEntity entity);
        Task<List<SpamReportEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
