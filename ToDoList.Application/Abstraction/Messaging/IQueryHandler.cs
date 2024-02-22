using MediatR;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
