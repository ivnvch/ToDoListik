using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.SingleTask;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;
using TaskStatus = ToDoList.Domain.Enum.TaskStatus;

namespace ToDoList.Application.Commands.SingleTaskCommand.Create
{
    public class CreateSingleTaskCommandHandler : ICommandHandler<CreateSingleTaskCommand, CreateSingleTaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSingleTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResult<CreateSingleTaskDto>> Handle(CreateSingleTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SingleTask singleTask = new SingleTask();

                singleTask.Name = request.Name;
                singleTask.Description = request.Description;
                singleTask.Status = TaskStatus.expectation;
                singleTask.DateCreated = DateTime.UtcNow;
                singleTask.TaskListId = request.TaskListId;
                //singleTask.UserId = getUserForEmail.Id;
                

                await _unitOfWork.SingleTaskRepository.CreateAsync(singleTask, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<CreateSingleTaskDto>
                {
                    Data = _mapper.Map<CreateSingleTaskDto>(singleTask),
                };

            }
            catch (Exception ex)
            {
                return new BaseResult<CreateSingleTaskDto>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
