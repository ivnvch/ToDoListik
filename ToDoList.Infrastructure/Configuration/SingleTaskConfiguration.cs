using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Infrastructure.Configuration
{
    public class SingleTaskConfiguration : IEntityTypeConfiguration<SingleTask>
    {
        public void Configure(EntityTypeBuilder<SingleTask> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(3000);


        }
    }
}
