using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.ProfileCommand
{
    public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, UserProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<BaseResult<UserProfileDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.UserRepository.FindByConditions(x => x.Id == request.Id, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                if (response is null)
                {
                    return new BaseResult<UserProfileDto>
                    {
                        ErrorMessage = "Profile not found",
                        ErrorCode = (int)ErrorCodes.ProfileNotFound
                    };
                }

                response.Name = request.Name;
                response.Email = request.Email;

                await _unitOfWork.UserRepository.UpdateAsync(response);
                await _unitOfWork.SaveChangesAsync(cancellationToken);


                return new BaseResult<UserProfileDto>
                {
                    Data = _mapper.Map<UserProfileDto>(response),
                };
            }
            catch (Exception)
            {
                return new BaseResult<UserProfileDto>
                {
                    ErrorMessage = "InternalServerError",
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }
}
