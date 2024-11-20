using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class ClientConfiguration : IEntityTypeConfiguration<Clients>
    {
        public void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.ClientID);

            builder.Property(c => c.ClientID)
                .HasColumnName("ClientID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(255)")
            .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(c => c.Phone)
            .HasColumnName("Phone")
            .HasColumnType("nvarchar(255)")
            .IsRequired();

            builder.Property(c => c.Company)
                .HasColumnName("Company")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnName("Address")
                 .HasColumnType("nvarchar(MAX)")
                 .IsRequired();

            builder.Property(c => c.CreateDate)
                .HasColumnName("CreateDate")
                .IsRequired();

            builder.HasMany(c => c.Projects)
                .WithOne(p => p.ClientRelation)
                .HasForeignKey(p => p.ClientID);
        }
    }
}
