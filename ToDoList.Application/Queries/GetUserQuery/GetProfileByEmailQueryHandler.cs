using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.GetUserQuery
{
    public class GetProfileByEmailQueryHandler : IQueryHandler<GetProfileByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProfileByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<UserDto>> Handle(GetProfileByEmailQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == request.Email, cancellationToken)
                    .Result.FirstOrDefaultAsync();

            return new BaseResult<UserDto>
            {
                Data = _mapper.Map<UserDto>(response),
            };
        }
    }
}
