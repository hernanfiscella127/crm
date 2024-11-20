using CRM.Domain.Entities;
using CRM.Infraestructure.Percistence.Config;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Percistence.Context
{
    public class CrmContext : DbContext
    {
        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Interactions> Interactions { get; set; }
        public DbSet<InteractionTypes> InteractionTypes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Domain.Entities.TaskStatus> TaskStatuses { get; set; }
        public DbSet<Domain.Entities.Tasks> Tasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<CampaignTypes> CampaignTypes { get; set; }
        public DbSet<Clients> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskStatusConfiguration());
            modelBuilder.ApplyConfiguration(new InteractionConfiguration());
            modelBuilder.ApplyConfiguration(new InteractionTypeConfiguration());
        }
    }
}
