using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReviewScoreRepository
    {
        Task AddAsync(ReviewScoreEntity entity);
        Task<ReviewScoreEntity> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
