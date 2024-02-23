using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;


namespace ToDoList.Application.Commands
{
    public record RegisterUserCommand(string email, string Password, string ConfirmPassword) : ICommand<UserDto>;

}
