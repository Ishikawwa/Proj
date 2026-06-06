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

        public Task<ReviewEntity?> GetByIdAsync(Guid id)
        {
            return context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<ReviewEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return context.Reviews.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public Task UpdateAsync(ReviewEntity entity)
        {
            context.Reviews.Update(entity);
            return context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.Reviews.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task BanAsync(Guid id)
        {
            return context.Reviews.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.IsBanned, true));
        }
    }
}