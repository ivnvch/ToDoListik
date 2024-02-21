using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ToDoList.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //DbSet<User> Users { get; set; } = null!;
        //DbSet<SingleTask> Tasks { get; set; } = null!;
        //DbSet<TaskList> TasksList { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
