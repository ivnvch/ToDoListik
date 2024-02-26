using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence.Configuration
{
    public class TaskStatusHistoryConfiguration : IEntityTypeConfiguration<TaskStatusHistory>
    {
        public void Configure(EntityTypeBuilder<TaskStatusHistory> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.TaskStatus)
               .IsRequired();

        }
    }
}
