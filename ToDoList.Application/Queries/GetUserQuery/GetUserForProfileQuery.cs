using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;

namespace ToDoList.Application.Queries.GetUserQuery
{
    public record GetUserForProfileQuery(string Email) : IQuery<UserProfileDto>;
}
