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

        public async Task<ReviewScoreEntity> GetByIdAsync(Guid id)
        {
            return await context.ReviewScores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            ReviewScoreEntity entity = await context.ReviewScores.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.ReviewScores.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}