using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;
using ToDoList.Application.Extensions;
using ToDoList.Application.Queries.GetUserQuery;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;
using TaskStatus = ToDoList.Domain.Enum.TaskStatus;

namespace ToDoList.Application.Commands.SingleTaskCommand.Update
{
    public class UpdateSingleTaskCommandHandler : ICommandHandler<UpdateSingleTaskCommand, UpdateSingleTaskDto>
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
            var getTask = await _unitOfWork.SingleTaskRepository.FindByConditions(x => x.Id == request.TaskId, cancellationToken)
                .Result.Where(x => x.TaskListId == request.TaskListId).FirstOrDefaultAsync();

            if (getTask is null)
            {
                return new BaseResult<UpdateSingleTaskDto>
                {
                    ErrorMessage = "Не найдена задача для обновления",
                    ErrorCode = (int)ErrorCodes.SingleTaskNotFound,
                };
            }

            getTask.Name = request.Name;
            getTask.Status = request.Status.ToEnum<TaskStatus>();
            getTask.Description = request.Description;

            await _unitOfWork.SingleTaskRepository.UpdateAsync(getTask);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult<UpdateSingleTaskDto>
            {
                Data = _mapper.Map<UpdateSingleTaskDto>(getTask),
            };

        }
    }
}
