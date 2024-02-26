using ToDoList.Domain.Entity;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories.Abstract;

namespace ToDoList.Persistence.Repositories
{
    public class SingleTaskRepository : BaseRepository<SingleTask>, ISingleTaskRepository
    {
        public SingleTaskRepository(DataContext context) : base(context)
        {
        }
    }
}
