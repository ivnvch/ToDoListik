using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.TaskListCommand.Update
{
    public sealed class UpdateTaskListCommandHandler : ICommandHandler<UpdateTaskListCommand, TaskListDto>
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
                var response = await _unitOfWork.TaskListRepository.FindByConditions(x => x.Id == request.Id, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                if (response is null)
                {
                    return new BaseResult<TaskListDto> 
                    { 
                        ErrorCode = (int)ErrorCodes.TaskListNotFound,
                        ErrorMessage = "Список задач не найден",
                    };
                }

                response.Description = request.Description;
                response.Name = request.Name;

                await _unitOfWork.TaskListRepository.UpdateAsync(response);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<TaskListDto>
                {
                    Data = _mapper.Map<TaskListDto>(response),
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
