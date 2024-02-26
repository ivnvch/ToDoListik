using MediatR;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, BaseResult<TResponse>> where TQuery : IQuery<TResponse>
    {
    }
}
