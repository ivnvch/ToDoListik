using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;

namespace ToDoList.Application.Commands.SingleTaskCommand.Delete
{
    public record DeleteSingleTaskCommand(long taskId, long taskListId): ICommand<bool>;
}
