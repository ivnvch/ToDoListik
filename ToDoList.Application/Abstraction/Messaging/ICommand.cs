﻿using MediatR;

namespace ToDoList.Application.Abstraction.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
