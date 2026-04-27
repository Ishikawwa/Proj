using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UpdateInstitutionRequestRepository(ApplicationContext context) : IUpdateInstitutionRequestRepository
    {
        public async Task AddAsync(UpdateInstitutionRequestEntity entity)
        {
            await context.UpdateInstitutionRequests.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<UpdateInstitutionRequestEntity>> GetAllAsync()
        {
            return await context.UpdateInstitutionRequests.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            UpdateInstitutionRequestEntity entity = await context.UpdateInstitutionRequests
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.UpdateInstitutionRequests.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}