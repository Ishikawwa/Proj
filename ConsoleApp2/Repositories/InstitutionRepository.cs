using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InstitutionRepository(ApplicationContext context) : IInstitutionRepository
    {
        public async Task AddAsync(InstitutionEntity entity)
        {
            await context.Institutions.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<InstitutionEntity> GetByIdAsync(Guid id)
        {
            return await context.Institutions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<InstitutionEntity>> GetAllAsync()
        {
            return await context.Institutions.ToListAsync();
        }

        public async Task<List<InstitutionEntity>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await context.Institutions.Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public async Task UpdateAsync(InstitutionEntity entity)
        {
            context.Institutions.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            InstitutionEntity entity = await context.Institutions.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Institutions.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task AssignOwnerAsync(Guid institutionId, Guid ownerId)
        {
            InstitutionEntity entity = await context.Institutions.FirstOrDefaultAsync(x => x.Id == institutionId);

            if (entity != null)
            {
                entity.OwnerId = ownerId;
                await context.SaveChangesAsync();
            }
        }
    }
}