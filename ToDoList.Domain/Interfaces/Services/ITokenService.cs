using System.Security.Claims;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;

namespace ToDoList.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();
        Task<BaseResult<TokenDto>> RefreshToken(TokenDto tokenDto, CancellationToken cancellationToken);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    }
}
