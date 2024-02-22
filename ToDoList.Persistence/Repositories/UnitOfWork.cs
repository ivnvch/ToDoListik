using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private ISingleTaskRepository _singleTaskRepository;
        private ITaskListRepository _taskListRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ISingleTaskRepository SingleTaskRepository
        {
            get
            {
                if (_singleTaskRepository == null)
                {
                    _singleTaskRepository = new SingleTaskRepository(_context);
                }

                return _singleTaskRepository;
            }
        }

        public ITaskListRepository TaskListRepository
        {
            get
            {
                if (_taskListRepository == null)
                {
                    _taskListRepository = new TaskListRepository(_context);
                }

                return _taskListRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
