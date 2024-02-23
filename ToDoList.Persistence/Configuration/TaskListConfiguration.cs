using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence.Configuration
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
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

            builder.HasMany(x => x.SingleTasks)
                .WithOne(x => x.TaskList)
                .HasForeignKey(x => x.TaskListId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
