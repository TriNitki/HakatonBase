using MediatR;
using Pkg.UseCases;
using Base.UseCases.Abstractions;

namespace Base.UseCases.Commands.Auth.InvalidateRefreshToken;

/// <summary>
/// Обработчик команды инвалидации токена обновления.
/// </summary>
public class InvalidateRefreshTokenCommandHandler : IRequestHandler<InvalidateRefreshTokenCommand, Result<Unit>>
{
    private readonly ITokenRepository _refreshTokenRepository;

    public InvalidateRefreshTokenCommandHandler(ITokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<Unit>> Handle(InvalidateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        if (token == null)
            return Result<Unit>.Invalid("Token wasn't found");

        await _refreshTokenRepository.UseByTokenAsync(token.Token);
        return Result<Unit>.Empty();
    }
}
