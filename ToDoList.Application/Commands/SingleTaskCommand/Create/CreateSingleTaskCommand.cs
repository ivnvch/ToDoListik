using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;

namespace ToDoList.Application.Commands.SingleTaskCommand.Create
{
    public record CreateSingleTaskCommand(long TaskListId, string Name, string Description, DateTime DateCreated) : ICommand<CreateSingleTaskDto>;
}
