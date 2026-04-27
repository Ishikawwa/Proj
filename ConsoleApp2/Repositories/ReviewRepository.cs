using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ReviewRepository(ApplicationContext context) : IReviewRepository
    {
        public async Task AddAsync(ReviewEntity entity)
        {
            await context.Reviews.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<ReviewEntity> GetByIdAsync(Guid id)
        {
            return await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ReviewEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return await context.Reviews.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public async Task UpdateAsync(ReviewEntity entity)
        {
            context.Reviews.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            ReviewEntity entity = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Reviews.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task BanAsync(Guid id)
        {
            ReviewEntity entity = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Reviews.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}