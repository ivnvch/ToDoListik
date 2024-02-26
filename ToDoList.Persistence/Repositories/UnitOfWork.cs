using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private ISingleTaskRepository _singleTaskRepository;
        private ITaskListRepository _taskListRepository;
        private IUserRepository _userRepository;
        private IUserTokenRepository _userTokenRepository;
        private ITaskStatusHistoryRepository _taskStatusHistoryRepository;

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

        public ITaskStatusHistoryRepository TaskStatusHistoryRepository
        {
            get
            {
                if (_taskStatusHistoryRepository == null)
                {
                    _taskStatusHistoryRepository = new TaskStatusHistoryRepository(_context);
                }

                return _taskStatusHistoryRepository;
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

        public IUserTokenRepository UserTokenRepository
        {
            get
            {
                if (_userTokenRepository == null)
                {
                    _userTokenRepository = new UserTokenRepository(_context);
                }

                return _userTokenRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
