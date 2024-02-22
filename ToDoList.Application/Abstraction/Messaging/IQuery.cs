using MediatR;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
