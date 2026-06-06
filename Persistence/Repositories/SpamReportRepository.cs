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

        public Task<SpamReportEntity> GetByIdAsync(Guid id)
        {
            return context.SpamReports.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task DeleteAsync(Guid id)
        {
            return context.SpamReports.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task<List<SpamReportEntity>> GetPendingAsync()
        {
            return context.SpamReports.Where(x => !x.IsProcessed).ToListAsync();
        }

        public Task MarkAsProcessedAsync(Guid id)
        {
            return context.SpamReports.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.IsProcessed, true));
        }
    }
}