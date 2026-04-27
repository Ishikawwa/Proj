using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(ReviewEntity entity);
        Task<ReviewEntity> GetByIdAsync(Guid id);
        Task<List<ReviewEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task UpdateAsync(ReviewEntity entity);
        Task DeleteAsync(Guid id);
        Task BanAsync(Guid id);
    }
}
