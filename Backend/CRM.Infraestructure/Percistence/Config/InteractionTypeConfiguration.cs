using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class InteractionTypeConfiguration : IEntityTypeConfiguration<InteractionTypes>
    {
        public void Configure(EntityTypeBuilder<InteractionTypes> builder)
        {
            builder.ToTable("InteractionTypes");

            builder.HasKey(it => it.Id);

            builder.Property(it => it.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(it => it.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(25)")
                .IsRequired();

            builder.HasData(
                new InteractionTypes
                {
                    Id = 1,
                    Name = "Initial Meeting"
                },
                new InteractionTypes
                {
                    Id = 2,
                    Name = "Phone call"
                },
                new InteractionTypes
                {
                    Id = 3,
                    Name = "Email"
                },
                new InteractionTypes
                {
                    Id = 4,
                    Name = "Presentation of Results"
                });
        }
    }
}
