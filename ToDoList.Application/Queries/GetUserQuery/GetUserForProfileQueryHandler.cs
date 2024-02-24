using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.GetUserQuery
{
    public class GetUserForProfileQueryHandler : IQueryHandler<GetUserForProfileQuery, UserProfileDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserForProfileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<UserProfileDto>> Handle(GetUserForProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var searchProfile = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == request.Email, cancellationToken).Result.FirstOrDefaultAsync();

                var profileDto = _mapper.Map<UserProfileDto>(searchProfile);

                return new BaseResult<UserProfileDto>
                {
                    Data = profileDto,
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<UserProfileDto>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = $"Внутреняя ошибка: {ex.Message}",
                };
            }
        }
    }
}
