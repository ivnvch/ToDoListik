using ToDoList.Domain.Entity;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories.Abstract;

namespace ToDoList.Persistence.Repositories
{
    public class TaskListRepository : BaseRepository<TaskList>, ITaskListRepository
    {
        public TaskListRepository(DataContext context) : base(context)
        {
        }
    }
}
