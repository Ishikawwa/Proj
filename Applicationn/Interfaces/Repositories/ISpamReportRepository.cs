using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISpamReportRepository
    {
        Task AddAsync(SpamReportEntity entity);
        Task<List<SpamReportEntity>> GetPendingAsync();
        Task MarkAsProcessedAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<SpamReportEntity> GetByIdAsync(Guid id);
    }
}
