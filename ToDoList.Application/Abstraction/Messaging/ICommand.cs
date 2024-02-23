using MediatR;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface ICommand<TResponse> : IRequest<BaseResult<TResponse>>
    {
    }
}
