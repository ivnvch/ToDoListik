using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.Interfaces.Services;
using ToDoList.Domain.Result;
using ToDoList.Domain.Settings;

namespace ToDoList.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IUnitOfWork unitOfWork, IOptions<JwtSettings> options)
        {
            _unitOfWork = unitOfWork;
            _jwtKey = options.Value.JwtKey;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(10), credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];
            using var randomNumbersGenerator = RandomNumberGenerator.Create();
            randomNumbersGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = true,

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return claimsPrincipal;
        }

        public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto tokenDto, CancellationToken cancellationToken)
        {
            string accessToken = tokenDto.AccessToken;
            string refreshToken = tokenDto.RefreshToken;

            var claimsPrincipals = GetPrincipalFromExpiredToken(accessToken);
            string userName = claimsPrincipals.Identity?.Name;
            var user = await _unitOfWork.UserRepository.FindByConditions(x => x.Email == userName, cancellationToken)
                .Result.Include(x => x.Token).FirstOrDefaultAsync();

            if (user is null || user.Token.RefreshToken != refreshToken
                || user.Token.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new BaseResult<TokenDto>
                {
                    ErrorMessage = "InvalidClientRequest",
                };
            }

            var newAccessToken = GenerateAccessToken(claimsPrincipals.Claims);
            var newRefreshToken = GenerateRefreshToken();

            user.Token.RefreshToken = newRefreshToken;
            await _unitOfWork.UserRepository.UpdateAsync(user);

            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                }
            };
        }

    }
}
