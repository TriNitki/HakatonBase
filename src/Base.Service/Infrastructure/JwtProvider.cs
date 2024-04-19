using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Base.Core;
using Base.Core.Options;
using Base.Core.Providers;
using Base.UseCases.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Base.Service.Infrastructure;

/// <inheritdoc/>
public class JwtProvider : IJwtProvider
{
    private readonly SecurityOptions _options;
    private readonly ITokenRepository _refreshTokenRepository;

    /// <inheritdoc/>
    public JwtProvider(IOptions<SecurityOptions> options, ITokenRepository refreshTokenRepository)
    {
        _options = options.Value;
        _refreshTokenRepository = refreshTokenRepository;
    }

    /// <inheritdoc/>
    public string GenerateAccess(long id, string nikname)
    {
        return Generate(id, nikname, DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeInMinutes));
    }

    /// <inheritdoc/>
    public async Task<RefreshToken> GenerateSaveRefreshAsync(long userId)
    {
        var token = new RefreshToken()
        {
            Token = Guid.NewGuid().ToString(),
            Expiration = DateTime.UtcNow.AddMinutes(_options.RefreshTokenLifetimeInMinutes),
            IsUsed = false,
            UserId = userId
        };
        await _refreshTokenRepository.CreateAsync(token);
        return token;
    }

    public async Task<RefreshToken?> GetRefreshByToken(string token)
    {
        return await _refreshTokenRepository.GetByTokenAsync(token);
    }

    public async Task UseRefreshByTokenAsync(string token)
    {
        await _refreshTokenRepository.UseByTokenAsync(token);
    }

    public bool Validate(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_options.SecretKey);

        IPrincipal principal;
        try
        {
            principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);
        }
        catch (SecurityTokenMalformedException)
        {
            return false;
        }

        return principal.Identity != null && principal.Identity.IsAuthenticated;
    }

    private string Generate(long id, string nikname, DateTime expiryDateTime)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Name, nikname)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            null,
            null,
            claims,
            null,
            expiryDateTime,
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
