using ToDoList.Domain.Entity;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories.Abstract;

namespace ToDoList.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
