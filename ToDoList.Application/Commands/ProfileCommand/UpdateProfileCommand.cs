using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;

namespace ToDoList.Application.Commands.ProfileCommand
{
    public record UpdateProfileCommand(long Id, string Email, string Name) : ICommand<UserProfileDto>;
}
