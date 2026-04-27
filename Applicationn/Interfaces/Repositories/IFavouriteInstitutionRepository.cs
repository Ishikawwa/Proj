using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFavouriteInstitutionRepository
    {
        Task AddAsync(FavouriteInstitutionEntity entity);
        Task<List<FavouriteInstitutionEntity>> GetByUserIdAsync(Guid userId);
        Task DeleteAsync(Guid id);
    }
}
