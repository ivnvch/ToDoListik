using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;


namespace ToDoList.Application.Commands.RegisterCommand
{
    public record RegisterUserCommand(string Email, string Password, string ConfirmPassword) : ICommand<UserDto>;

}
