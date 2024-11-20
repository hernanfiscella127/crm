using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks = CRM.Domain.Entities.Tasks;

namespace CRM.Infraestructure.Percistence.Config  //verificado
{
    public class TaskConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.TaskID);

            builder.Property(t => t.TaskID)
                .ValueGeneratedOnAdd()
                .HasColumnName("TaskID")
                .IsRequired();

            builder.Property(t => t.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

            builder.Property(t => t.DueDate)
                .HasColumnName("DueDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(t => t.CreateDate)
                .HasColumnName("CreateDate")
                .IsRequired();

            builder.Property(t => t.UpdateDate)
                .HasColumnName("UpdateDate")
                .IsRequired();

            builder.Property(t => t.ProjectID)
                .HasColumnName("ProjectID")
                .IsRequired();

            builder.Property(t => t.AssignedTo)
                .HasColumnName("AssignedTo")
                .IsRequired();

            builder.Property(t => t.Status)
            .HasColumnName("Status")
            .IsRequired();

            // Relaciones
            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.ProjectID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.User)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.AssignedTo)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TaskStatus)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.Status)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
