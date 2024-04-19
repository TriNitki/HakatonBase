using MediatR;
using UseCases;
using Base.Contracts;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Login;

namespace Base.UseCases.Commands.RefreshTokens;

/// <summary>
/// Обработчик команды обновления токенов авторизации.
/// </summary>
public class RefreshTokensCommandHandler : LoginBaseHandler, IRequestHandler<RefreshTokensCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;

    public RefreshTokensCommandHandler(
        IUserRepository userRepository,
        ITokenService tokenService) : base(tokenService)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Tokens>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _tokenService.DeactivateRefreshToken(request.RefreshToken);

        if (refreshToken is null)
        {
            return Result<Tokens>.Invalid("Refresh token is incorrect");
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId) ?? throw new NullReferenceException();

        if (user.IsBlocked)
        {
            // TODO: возвращать 401 статус код. Сделать кастомный результат логина.
            return Result<Tokens>.Invalid("User is blocked");
        }

        return await Handle(user);
    }
}
