using MediatR;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface IQuery<TResponse> : IRequest<BaseResult<TResponse>>
    {
    }
}
