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

        public async Task<List<FavouriteInstitutionEntity>> GetByUserIdAsync(Guid userId)
        {
            return await context.FavouriteInstitutions.Where(x => x.UserId == userId).ToListAsync();
        }   

        public async Task DeleteAsync(Guid id)
        {
            FavouriteInstitutionEntity entity = await context.FavouriteInstitutions.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.FavouriteInstitutions.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}