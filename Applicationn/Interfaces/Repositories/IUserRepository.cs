using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity entity);
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<List<UserEntity>> GetAllAsync();
        Task BanAsync(Guid id);
        Task MuteAsync(Guid id);
        Task DeleteAsync(Guid id);
    }

}
