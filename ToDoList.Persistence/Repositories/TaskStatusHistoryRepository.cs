using ToDoList.Domain.Entity;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories.Abstract;

namespace ToDoList.Persistence.Repositories
{
    public class TaskStatusHistoryRepository : BaseRepository<TaskStatusHistory>, ITaskStatusHistoryRepository
    {
        public TaskStatusHistoryRepository(DataContext context) : base(context)
        {
        }
    }
}
