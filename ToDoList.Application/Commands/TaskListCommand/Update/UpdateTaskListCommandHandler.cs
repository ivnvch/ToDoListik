using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Application.Dto.User;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.TaskListCommand.Update
{
    public class UpdateTaskListCommandHandler : ICommandHandler<UpdateTaskListCommand, TaskListDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<TaskListDto>> Handle(UpdateTaskListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await _unitOfWork.TaskListRepository.FindByConditions(x => x.Id == request.Id, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                if (taskList is null)
                {
                    return new BaseResult<TaskListDto> 
                    { 
                        ErrorCode = (int)ErrorCodes.TaskListNotFound,
                        ErrorMessage = "Список задач не найден",
                    };
                }

                taskList.Description = request.Description;
                taskList.Name = request.Name;

                await _unitOfWork.TaskListRepository.UpdateAsync(taskList);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<TaskListDto>
                {
                    Data = _mapper.Map<TaskListDto>(taskList),
                };
            }
            catch (Exception)
            {
                return new BaseResult<TaskListDto>
                {
                    ErrorMessage = "InternalServerError",
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }
}
