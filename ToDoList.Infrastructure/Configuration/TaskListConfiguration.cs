using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Infrastructure.Configuration
{
    internal class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(3000);

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.TaskList)
                .HasForeignKey(x => x.TaskList.Id)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
