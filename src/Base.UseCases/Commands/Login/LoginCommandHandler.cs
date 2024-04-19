using MediatR;
using Pkg.UseCases;
using Base.UseCases.Abstractions;
using Base.Core.Providers;
using Base.Core;
using Base.Contracts.Auth;

namespace Base.UseCases.Commands.Login;

/// <summary>
/// Обработчик команды логина.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IPasswordHashProvider passwordHashProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<Tokens>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByLoginPasswordAsync(
            request.Login,
            _passwordHashProvider.Encrypt(request.Password));

        if (user == null)
            return Result<Tokens>.Invalid("User wasn't found");

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
