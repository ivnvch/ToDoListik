﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Dto.User;
using ToDoList.Application.Extensions;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Result;



namespace ToDoList.Application.Commands.RegisterCommand
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "Password not confirm",
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsConfirmPassword,
                };
            }

            try
            {
                var response = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == request.Email, cancellationToken)
                    .Result.FirstOrDefaultAsync();

                if (response is not null)
                {
                    return new BaseResult<UserDto>
                    {
                        ErrorCode = (int)ErrorCodes.UserIsAlreadyExists,
                        ErrorMessage = "User is already exists",
                    };
                }

                response = new User()
                {
                    Email = request.Email,
                    Password = HashPasswordExtension.HashPassword(request.Password),
                };

                await _unitOfWork.UserRepository.CreateAsync(response, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new BaseResult<UserDto>
                {
                    Data = _mapper.Map<UserDto>(response),
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<UserDto>
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
