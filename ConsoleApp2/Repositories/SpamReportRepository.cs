using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SpamReportRepository(ApplicationContext context) : ISpamReportRepository
    {
        public async Task AddAsync(SpamReportEntity entity)
        {
            await context.SpamReports.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<SpamReportEntity>> GetAllAsync()
        {
            return await context.SpamReports.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            SpamReportEntity entity = await context.SpamReports.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.SpamReports.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}