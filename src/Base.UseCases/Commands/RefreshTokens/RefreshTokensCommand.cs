using MediatR;
using Pkg.UseCases;
using Base.Contracts;

namespace Base.UseCases.Commands.RefreshTokens;

/// <summary>
/// Команды обновления токенов авторизации.
/// </summary>
public class RefreshTokensCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Токен обновления.
    /// </summary>
    public string RefreshToken { get; }

    public RefreshTokensCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
