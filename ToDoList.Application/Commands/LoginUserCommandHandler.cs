using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Application.Abstraction.Messaging;
using ToDoList.Application.Extensions;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Interfaces.Services;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<BaseResult<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfWork.UserRepository
               .FindByConditions(x => x.Email == request.Email, cancellationToken)
                 .Result.FirstOrDefaultAsync();

                if (response is null)
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorCode = (int)ErrorCodes.UserNotFound,
                        ErrorMessage = "User not found",

                    };
                }

                var isVerifyPassword = IsVerifyPasswordExtension.IsVerifyPassword(response.Password, request.Password);

                if (!isVerifyPassword)
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorCode = (int)ErrorCodes.PasswordIsWrong,
                        ErrorMessage = "Password is wrong",

                    };
                }

                var userToken = await _unitOfWork.UserTokenRepository
                    .FindByConditions(x => x.UserId == response.Id, cancellationToken)
                      .Result.FirstOrDefaultAsync();

                var claims = new List<Claim>()
                 {
                    new Claim(ClaimTypes.Email, response.Email),
                    //new Claim(ClaimTypes., "User"),
                 };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                if (userToken is null)
                {
                    userToken = new UserToken()
                    {
                        UserId = response.Id,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                    };

                    await _unitOfWork.UserTokenRepository.CreateAsync(userToken, cancellationToken);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    userToken.RefreshToken = refreshToken;
                    userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);//нужно ли указывать время жизни refreshToken
                }

                return new BaseResult<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        RefreshToken = refreshToken,
                        AccessToken = accessToken,//????
                    },
                };

            }
            catch (Exception ex)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = $"You have some troubles {ex.Message}",
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }




        }
    }
}
