using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class CampaignTypeConfiguration : IEntityTypeConfiguration<CampaignTypes>
    {
        public void Configure(EntityTypeBuilder<CampaignTypes> builder)
        {
            builder.ToTable("CampaignTypes");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(ct => ct.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(25)")
                .IsRequired();

            builder.HasMany(ct => ct.Projects)
                .WithOne(p => p.CampaignTypeNavigation)
                .HasForeignKey(p => p.CampaignType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new CampaignTypes
                {
                    Id = 1,
                    Name = "SEO"
                },
                new CampaignTypes
                {
                    Id = 2,
                    Name = "PPC"
                },
                new CampaignTypes
                {
                    Id = 3,
                    Name = "Social Media"
                },
                new CampaignTypes
                {
                    Id = 4,
                    Name = "Email Marketing"
                });
        }
    }
}

