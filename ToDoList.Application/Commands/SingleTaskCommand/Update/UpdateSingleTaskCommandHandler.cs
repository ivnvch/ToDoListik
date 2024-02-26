using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;
using ToDoList.Application.Extensions;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;
using TaskStatus = ToDoList.Domain.Enum.TaskStatus;

namespace ToDoList.Application.Commands.SingleTaskCommand.Update
{
    public sealed class UpdateSingleTaskCommandHandler : ICommandHandler<UpdateSingleTaskCommand, UpdateSingleTaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSingleTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<UpdateSingleTaskDto>> Handle(UpdateSingleTaskCommand request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.SingleTaskRepository.FindByConditions(x => x.Id == request.TaskId, cancellationToken)
                  .Result.Where(x => x.TaskListId == request.TaskListId)
                    .FirstOrDefaultAsync();

            var taskStatusHistory = await _unitOfWork.TaskStatusHistoryRepository.FindByConditions(x => x.SingleTaskId == response.Id, cancellationToken)
                  .Result.FirstOrDefaultAsync();

            if (response is null)
            {
                return new BaseResult<UpdateSingleTaskDto>
                {
                    ErrorMessage = "Не найдена задача для обновления",
                    ErrorCode = (int)ErrorCodes.SingleTaskNotFound,
                };
            }

            response.Name = request.Name;
            response.Description = request.Description;
            
            if (request.Status.ToEnum<TaskStatus>() != response.Status)
            {
               response.Status = request.Status.ToEnum<TaskStatus>();

                TaskStatusHistory taskStatus = new TaskStatusHistory()
                {
                    DateTimeUpdated = DateTime.UtcNow,
                    SingleTaskId = response.Id,
                    TaskStatus = response.Status,
                };

               await _unitOfWork.TaskStatusHistoryRepository.CreateAsync(taskStatus, cancellationToken);
            }
            

            await _unitOfWork.SingleTaskRepository.UpdateAsync(response);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult<UpdateSingleTaskDto>
            {
                Data = _mapper.Map<UpdateSingleTaskDto>(response),
            };

        }
    }
}
