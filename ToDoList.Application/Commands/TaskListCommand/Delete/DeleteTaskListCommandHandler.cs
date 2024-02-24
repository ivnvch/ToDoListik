using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.TaskListCommand.Delete
{
    public class DeleteTaskListCommandHandler : ICommandHandler<DeleteTaskListCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<bool>> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.TaskListRepository.FindByConditions(x => x.Id == request.Id, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                 await _unitOfWork.TaskListRepository.DeleteAsync(response);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<bool>
                {
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<bool>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
