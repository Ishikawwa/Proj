using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IReviewScoreRepository
    {
        Task AddAsync(ReviewScoreEntity entity);
        Task<ReviewScoreEntity?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
