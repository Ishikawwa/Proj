using Domain.Entities;

public interface IReviewRepository
{
    Task AddAsync(ReviewEntity entity);
    Task<ReviewEntity?> GetByIdAsync(Guid id);
    Task<List<ReviewEntity>> GetByInstitutionIdAsync(Guid institutionId);
    Task UpdateAsync(ReviewEntity entity);
    Task DeleteAsync(Guid id);
    Task BanAsync(Guid id);
}