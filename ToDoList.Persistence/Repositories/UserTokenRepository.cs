using ToDoList.Domain.Entity;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories.Abstract;

namespace ToDoList.Persistence.Repositories
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(DataContext context) : base(context)
        {
        }
    }
}
