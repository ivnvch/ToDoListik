using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<TaskStatusHistory> TaskStatuses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SingleTask> SingleTasks { get; set; } = null!;
        public DbSet<TaskList> TasksList { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
