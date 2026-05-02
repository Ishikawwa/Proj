using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InstitutionRepository(ApplicationContext context) : IInstitutionRepository
    {
        public async Task AddAsync(InstitutionEntity entity)
        {
            await context.Institutions.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task<InstitutionEntity?> GetByIdAsync(Guid id)
        {
            return context.Institutions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<InstitutionEntity>> GetAllAsync()
        {
            return context.Institutions.ToListAsync();
        }

        public Task<List<InstitutionEntity>> GetByOwnerIdAsync(Guid ownerId)
        {
            return context.Institutions.Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public Task UpdateAsync(InstitutionEntity entity)
        {
            context.Institutions.Update(entity);
            return context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.Institutions.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task AssignOwnerAsync(Guid institutionId, Guid ownerId)
            => context.Institutions.Where(x => x.Id == institutionId).ExecuteUpdateAsync(s => s.SetProperty(x => x.OwnerId, ownerId));
    }
}