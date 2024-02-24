using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;

namespace ToDoList.Application.Commands.SingleTaskCommand.Create
{
    public record CreateSingleTaskCommand(long id, string Email, string Name, string Description, DateTime DateCreated, string TaskStatus) : ICommand<SingleTaskDto>
    {
    }
}
