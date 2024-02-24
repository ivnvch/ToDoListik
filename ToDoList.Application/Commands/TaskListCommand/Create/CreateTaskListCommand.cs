using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.TaskList;

namespace ToDoList.Application.Commands.TaskListCommand.Create
{
    public record CreateTaskListCommand(string Name, string Description, string Email) : ICommand<TaskListDto>;
}
