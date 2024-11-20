using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infraestructure.Percistence.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserID);  // Llave primaria

            builder.Property(u => u.UserID)
                .HasColumnName("UserId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.HasData(
                new Users
                {
                    UserID = 1,
                    Name = "Joe Done",
                    Email = "jdone@marketing.com"
                },
                new Users
                {
                    UserID = 2,
                    Name = "Nill Armstrong",
                    Email = "namstrong@marketing.com"
                },
                new Users
                {
                    UserID = 3,
                    Name = "Marlyn Morales",
                    Email = "mmorales@marketing.com"
                },
                new Users
                {
                    UserID = 4,
                    Name = "Antony Orúe",
                    Email = "aorue@marketing.com"
                },
                new Users
                {
                    UserID = 5,
                    Name = "Jazmin Fernandez",
                    Email = "jfernandez@marketing.com"
                });

        }
    }

}
