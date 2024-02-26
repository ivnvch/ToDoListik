using ToDoList.Application.Abstraction.Messaging;

namespace ToDoList.Application.Commands.SingleTaskCommand.Delete
{
    public record DeleteSingleTaskCommand(long taskId, long taskListId): ICommand<bool>;
}
