using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;

namespace ToDoList.Application.Commands.SingleTaskCommand.Update
{
    public record UpdateSingleTaskCommand(long TaskId,long TaskListId, string Name, string Description, string Status) : ICommand<UpdateSingleTaskDto>;
}
