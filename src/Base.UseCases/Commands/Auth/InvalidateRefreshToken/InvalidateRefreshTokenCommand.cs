using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Commands.Auth.InvalidateRefreshToken;

/// <summary>
/// Команда инвалидации токена обновления.
/// </summary>
public class InvalidateRefreshTokenCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Токен обновления.
    /// </summary>
    public string RefreshToken { get; }

    public InvalidateRefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
