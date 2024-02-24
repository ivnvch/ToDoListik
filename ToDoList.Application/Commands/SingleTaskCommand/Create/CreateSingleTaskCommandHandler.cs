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
    public class CreateSingleTaskCommandHandler : ICommandHandler<CreateSingleTaskCommand, SingleTaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSingleTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResult<SingleTaskDto>> Handle(CreateSingleTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getUserForEmail = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == request.Email, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                SingleTask singleTask = new SingleTask() 
                { 
                    Name = request.Name,
                    Description = request.Description,
                    Status = TaskStatus.expectation,
                    DateCreated = DateTime.UtcNow,
                    TaskListId = getUserForEmail.TaskList

                };
                
                

                await _unitOfWork.SingleTaskRepository.CreateAsync(singleTask, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<SingleTaskDto>
                {
                    Data = _mapper.Map<SingleTaskDto>(singleTask),
                };

            }
            catch (Exception ex)
            {
                return new BaseResult<SingleTaskDto>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
