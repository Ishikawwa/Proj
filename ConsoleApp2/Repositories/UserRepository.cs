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

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task BanAsync(Guid id)
        {
            UserEntity entity = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Users.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task MuteAsync(Guid id)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            UserEntity entity = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Users.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}