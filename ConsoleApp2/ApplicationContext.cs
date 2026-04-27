using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<InstitutionEntity> Institutions { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<ReviewScoreEntity> ReviewScores { get; set; }
        public DbSet<SpamReportEntity> SpamReports { get; set; }
        public DbSet<FavouriteInstitutionEntity> FavouriteInstitutions { get; set; }
        public DbSet<VisitingEntity> Visitings { get; set; }
        public DbSet<ViewingEntity> Viewings { get; set; }
        public DbSet<WorkingHoursEntity> WorkingHours { get; set; }
        public DbSet<InstitutionLabelEntity> InstitutionLabels { get; set; }
        public DbSet<OwnerRequestEntity> OwnerRequests { get; set; }
        public DbSet<UpdateInstitutionRequestEntity> UpdateInstitutionRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("");
        }
    }
}
