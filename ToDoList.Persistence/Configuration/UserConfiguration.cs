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

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.User.Id)
                .HasPrincipalKey(x => x.Id);

            builder.HasMany(x => x.TaskLists)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.User.Id)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
