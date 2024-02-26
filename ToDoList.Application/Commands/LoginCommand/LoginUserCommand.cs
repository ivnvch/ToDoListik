using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Domain.Dto;


namespace ToDoList.Application.Commands.LoginCommand
{
    public record LoginUserCommand(string Email, string Password) : ICommand<TokenDto>;
}
