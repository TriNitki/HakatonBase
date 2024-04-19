using MediatR;
using UseCases;
using Base.UseCases.Abstractions;

namespace Base.UseCases.Commands.InvalidateRefreshToken;

/// <summary>
/// Обработчик команды инвалидации токена обновления.
/// </summary>
public class InvalidateRefreshTokenCommandHandler : IRequestHandler<InvalidateRefreshTokenCommand, Result<Unit>>
{
    private readonly ITokenService _tokenService;

    public InvalidateRefreshTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<Result<Unit>> Handle(InvalidateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await _tokenService.DeactivateRefreshToken(request.RefreshToken);
        return Result<Unit>.Empty();
    }
}
