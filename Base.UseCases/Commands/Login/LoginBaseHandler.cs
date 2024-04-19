using UseCases;
using Base.Contracts;
using Base.Core;
using Base.UseCases.Abstractions;

namespace Base.UseCases.Commands.Login;

/// <summary>
/// Базовый обработчик авторизации.
/// </summary>
public abstract class LoginBaseHandler
{
    public readonly ITokenService _tokenService;

    public LoginBaseHandler(ITokenService tokenService)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    /// <summary>
    /// Авторизовать пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <returns> Результат авторизации. </returns>
    public async Task<Result<Tokens>> Handle(AuthUser user)
    {
        string accessToken = _tokenService.GenerateAccessToken(user);
        RefreshToken refreshToken = await _tokenService.GenerateRefreshToken(user.Id);

        return Result<Tokens>.Success(
            new Tokens(accessToken, refreshToken.Token, refreshToken.Expiration));
    }
}
