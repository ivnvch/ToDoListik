﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence.Configuration
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

            builder.HasMany(x => x.TaskStatusHistory)
                .WithOne(x => x.SingleTask)
                .HasForeignKey(x => x.SingleTaskId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
