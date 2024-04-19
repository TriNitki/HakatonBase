using MediatR;
using Pkg.UseCases;
using Base.UseCases.Abstractions;
using Base.Core.Providers;
using Base.Core;
using Base.Contracts.Auth;

namespace Base.UseCases.Commands.RefreshTokens;

/// <summary>
/// Обработчик команды обновления токенов авторизации.
/// </summary>
public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokensCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<Tokens>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var token = await _jwtProvider.GetRefreshByToken(request.RefreshToken);
        if (token == null)
            return Result<Tokens>.Invalid("Token wasn't found");

        if (token.IsUsed)
            return Result<Tokens>.Invalid("Token is already used");

        var user = await _userRepository.GetByIdAsync(token.UserId) ?? throw new NullReferenceException();

        await _jwtProvider.UseRefreshByTokenAsync(token.Token);

        string accessToken = _jwtProvider.GenerateAccess(user.Id, user.Nickname);
        RefreshToken refreshToken = await _jwtProvider.GenerateSaveRefreshAsync(user.Id);

        return Result<Tokens>.Success(
            new Tokens()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshExpiration = refreshToken.Expiration
            });
    }
}
