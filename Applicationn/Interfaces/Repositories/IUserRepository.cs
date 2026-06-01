using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity entity);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<List<UserEntity>> GetAllAsync();
        Task BanAsync(Guid id);
        Task MuteAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<UserEntity?> GetByVkIdAsync(string vkId);
        Task UpdateAsync(UserEntity user);
    }

}
