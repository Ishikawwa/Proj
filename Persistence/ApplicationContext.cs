using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstitutionLabelEntity>()
                .HasKey(x => new { x.InstitutionId, x.Label });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProjectDb;Username=postgres;Password=admin");
        }
    }
}
