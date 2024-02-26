using ToDoList.Application.Abstraction.Messaging;

namespace ToDoList.Application.Commands.TaskListCommand.Delete
{
    public record DeleteTaskListCommand(long Id, long NewListId) : ICommand<bool>
    {
    }
}
