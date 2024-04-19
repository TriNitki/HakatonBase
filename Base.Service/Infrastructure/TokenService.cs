using Base.Core;
using Base.Service.Options;
using Base.UseCases.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MNX.SecurityManagement.Service.Infrastructure;

/// <summary>
/// Реализация <see cref="ITokenService"/>.
/// </summary>
public class TokenService : ITokenService
{
    /// <summary>
    /// Параметры безопасности.
    /// </summary>
    private readonly SecurityOptions _options;

    /// <summary>
    /// Репозиторий для доступа к токенам обновления.
    /// </summary>
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenService(IOptions<SecurityOptions> options, IRefreshTokenRepository refreshTokenRepository)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
    }

    /// <inheritdoc/>
    public async Task<RefreshToken?> DeactivateRefreshToken(string token)
    {
        var tokenModel = await _refreshTokenRepository.GetByTokenAsync(token);

        if (tokenModel is null || !tokenModel.IsValid())
        {
            return null;
        }

        await _refreshTokenRepository.DeactivateAsync(tokenModel);
        return tokenModel;
    }

    /// <inheritdoc/>
    public string GenerateAccessToken(AuthUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: user.GetClaims(),
                notBefore: null,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeInMinutes),
                signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    /// <inheritdoc/>
    public async Task<RefreshToken> GenerateRefreshToken(long userId)
    {
        var expiration = DateTime.UtcNow.AddMinutes(_options.RefreshTokenLifetimeInMinutes);
        // todo: Guid норм?
        var token = new RefreshToken(Guid.NewGuid().ToString(), expiration, userId);

        await _refreshTokenRepository.CreateAsync(token);
        return token;
    }
}
