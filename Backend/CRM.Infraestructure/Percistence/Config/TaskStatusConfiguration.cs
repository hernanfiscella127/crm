using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskStatus = CRM.Domain.Entities.TaskStatus;

namespace CRM.Infraestructure.Percistence.Config
{
    public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.ToTable("TaskStatus");

            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id")
            .IsRequired();

            builder.Property(ts => ts.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(25)");

            //todo: revisar si configurar tambien en esta entidad es necesario.
            builder.HasMany(ts => ts.Tasks)
             .WithOne(t => t.TaskStatus)
             .HasForeignKey(t => t.Status)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new TaskStatus
                {
                    Id = 1,
                    Name = "Pending"
                },
                new TaskStatus
                {
                    Id = 2,
                    Name = "In Progress"
                },
                new TaskStatus
                {
                    Id = 3,
                    Name = "Blocked"
                },
                new TaskStatus
                {
                    Id = 4,
                    Name = "Done"
                },
                new TaskStatus
                {
                    Id = 5,
                    Name = "Cancel"
                });

        }
    }
}
