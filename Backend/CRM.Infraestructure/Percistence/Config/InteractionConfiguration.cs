using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class InteractionConfiguration : IEntityTypeConfiguration<Interactions>
    {
        public void Configure(EntityTypeBuilder<Interactions> builder)
        {
            builder.ToTable("Interactions");

            builder.HasKey(i => i.InteractionID);

            builder.Property(i => i.InteractionID)
                .ValueGeneratedOnAdd()
                .HasColumnName("InteractionID")
                .IsRequired();

            builder.Property(i => i.ProjectID)
                .HasColumnName("ProjectID")
                .IsRequired();

            builder.Property(i => i.InteractionType)
                .HasColumnName("InteractionType")
                .IsRequired();

            builder.Property(i => i.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(i => i.Notes)
                .HasColumnName("Notes")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            // Relaciones
            builder.HasOne(i => i.Project)
                    .WithMany(p => p.Interactions)
                    .HasForeignKey(i => i.ProjectID)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.InteractionTypes)
                    .WithMany(it => it.Interactions)
                    .HasForeignKey(i => i.InteractionType)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
