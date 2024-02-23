namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ISingleTaskRepository SingleTaskRepository { get; }
        ITaskListRepository TaskListRepository { get; }
        IUserRepository UserRepository { get; }
        IUserTokenRepository UserTokenRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
