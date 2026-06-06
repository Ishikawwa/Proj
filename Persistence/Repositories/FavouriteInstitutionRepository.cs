using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class FavouriteInstitutionRepository(ApplicationContext context) : IFavouriteInstitutionRepository
    {
        public async Task AddAsync(FavouriteInstitutionEntity entity)
        {
            await context.FavouriteInstitutions.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task<List<FavouriteInstitutionEntity>> GetByUserIdAsync(Guid userId)
        {
            return context.FavouriteInstitutions.Where(x => x.UserId == userId).ToListAsync();
        }

        public Task<FavouriteInstitutionEntity> GetByIdAsync(Guid id)
        {
            return context.FavouriteInstitutions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task DeleteAsync(Guid id)
        {
            return context.FavouriteInstitutions.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}