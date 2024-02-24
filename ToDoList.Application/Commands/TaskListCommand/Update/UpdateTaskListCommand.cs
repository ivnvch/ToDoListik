using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.TaskList;

namespace ToDoList.Application.Commands.TaskListCommand.Update
{
    public record UpdateTaskListCommand(long Id, string Name, string Description) : ICommand<TaskListDto>;
}
