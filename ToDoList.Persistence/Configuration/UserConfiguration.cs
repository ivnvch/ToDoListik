using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            //builder.HasMany(x => x.Tasks)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId)
            //    .HasPrincipalKey(x => x.Id)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TaskList)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(new List<User>
            //{
            //    new User()
            //    {
            //        Id = Guid.NewGuid(),
            //        Email = "whyNot",
            //        Password = "password",
            //        Name = "Dmitry"
            //    }
            //});
        }
    }
}
