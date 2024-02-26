using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.SingleTaskCommand.Delete
{
    public sealed class DeleteSingleTaskCommandHandler : ICommandHandler<DeleteSingleTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSingleTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<bool>> Handle(DeleteSingleTaskCommand request, CancellationToken cancellationToken)//globalException may be
        {
            var response = await _unitOfWork.SingleTaskRepository.FindByConditions(x => x.Id == request.taskId, cancellationToken)
                .Result.Where(x => x.TaskListId == request.taskListId)
                   .FirstOrDefaultAsync();

            if (response is null)
            {
                return new BaseResult<bool>
                { 
                    ErrorCode = (int)ErrorCodes.SingleTaskNotFound,
                    ErrorMessage = "Задача не найдена",
                };
            }

            await _unitOfWork.SingleTaskRepository.DeleteAsync(response);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult<bool>
            {
                Data = true,
            };
        }
    }
}
