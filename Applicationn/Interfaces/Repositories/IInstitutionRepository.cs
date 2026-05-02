using Domain.Entities;

public interface IInstitutionRepository
{
    Task AddAsync(InstitutionEntity entity);
    Task<InstitutionEntity?> GetByIdAsync(Guid id);
    Task<List<InstitutionEntity>> GetAllAsync();
    Task<List<InstitutionEntity>> GetByOwnerIdAsync(Guid ownerId);
    Task UpdateAsync(InstitutionEntity entity);
    Task DeleteAsync(Guid id);
    Task AssignOwnerAsync(Guid institutionId, Guid ownerId);
}