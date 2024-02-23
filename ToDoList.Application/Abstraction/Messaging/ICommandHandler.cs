using MediatR;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, BaseResult<TResponse>> where TCommand : ICommand<TResponse>
    {
    }

}
