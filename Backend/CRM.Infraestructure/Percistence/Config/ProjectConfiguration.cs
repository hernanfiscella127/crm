using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Projects>
    {
        public void Configure(EntityTypeBuilder<Projects> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.ProjectID);

            builder.Property(p => p.ProjectID)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProjectID")
                .IsRequired();

            builder.Property(p => p.ProjectName)
                .HasColumnName("ProjectName")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(p => p.CampaignType)
                .HasColumnName("CampaignType")
                .IsRequired();

            builder.Property(p => p.ClientID)
                .HasColumnName("ClientID")
                .IsRequired();

            builder.Property(p => p.StartDate)
                .HasColumnName("StartDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.EndDate)
                .HasColumnName("EndDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.CreateDate)
                .HasColumnName("CreateDate")
                .IsRequired();
            builder.Property(p => p.UpdateDate)
               .HasColumnName("UpdateDate")
               .IsRequired();

            // Relaciones
            builder.HasOne(p => p.CampaignTypeNavigation)
                    .WithMany(ct => ct.Projects)
                    .HasForeignKey(p => p.CampaignType)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ClientRelation)
                    .WithMany(ct => ct.Projects)
                    .HasForeignKey(p => p.ClientID)
                    .OnDelete(DeleteBehavior.Cascade);

            //todo: revisar si las siguientes configuraciones eran necesarias.
            builder.HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Interactions)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
