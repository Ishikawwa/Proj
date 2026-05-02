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

        public Task<List<UpdateInstitutionRequestEntity>> GetAllAsync()
        {
            return context.UpdateInstitutionRequests.ToListAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.UpdateInstitutionRequests.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}