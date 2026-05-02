using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ReviewScoreRepository(ApplicationContext context) : IReviewScoreRepository
    {
        public async Task AddAsync(ReviewScoreEntity entity)
        {
            await context.ReviewScores.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task<ReviewScoreEntity?> GetByIdAsync(Guid id)
        {
            return context.ReviewScores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task DeleteAsync(Guid id)
        {
            return context.ReviewScores.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}