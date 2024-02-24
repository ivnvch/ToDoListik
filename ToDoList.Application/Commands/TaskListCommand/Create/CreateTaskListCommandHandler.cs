using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.TaskListCommand.Create
{
    public class CreateTaskListCommandHandler : ICommandHandler<CreateTaskListCommand, TaskListDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTaskListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResult<TaskListDto>> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getUserForEmail = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == request.Email, cancellationToken).Result.FirstOrDefaultAsync();

                TaskList taskList = new TaskList();
                taskList.Name = request.Name;
                taskList.Description = request.Description;
                taskList.UserId = getUserForEmail.Id;

                await _unitOfWork.TaskListRepository.CreateAsync(taskList, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<TaskListDto>
                {
                    Data = _mapper.Map<TaskListDto>(taskList),
                };

            }
            catch (Exception ex)
            {
                return new BaseResult<TaskListDto>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
