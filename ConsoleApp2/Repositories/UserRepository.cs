using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
        public async Task AddAsync(UserEntity entity)
        {
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<UserEntity>> GetAllAsync()
        {
            return context.Users.ToListAsync();
        }

        public Task BanAsync(Guid id)
        {
            return context.Users.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.IsBanned, true));
        }

        public Task MuteAsync(Guid id)
        {
            return context.Users.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.IsMuted, true));
        }

        public Task DeleteAsync(Guid id)
        {
            return context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}