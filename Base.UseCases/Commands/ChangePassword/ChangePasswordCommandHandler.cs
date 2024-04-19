using MediatR;
using Microsoft.Extensions.Options;
using UseCases;
using Base.Contracts;
using Base.Core.Options;
using Base.Core.Services;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Login;

namespace Base.UseCases.Commands.ChangePassword;

/// <summary>
/// Обработчик команды изменения пароля.
/// </summary>
public class ChangePasswordCommandHandler : LoginBaseHandler, IRequestHandler<ChangePasswordCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly PasswordOptions _passwordOptions;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<PasswordOptions> passwordOptions,
        ITokenService tokenService) : base(tokenService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordOptions = passwordOptions.Value ?? throw new ArgumentNullException(nameof(passwordOptions));
    }

    public async Task<Result<Tokens>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Resolve(
            request.Login,
            CryptographyService.HashPassword(request.Password, _passwordOptions.Salt));

        if (user is null)
        {
            return Result<Tokens>.Invalid("Invalid password or login");
        }

        user.SetPassword(request.NewPassword, _passwordOptions);
        await _userRepository.UpdateAsync(user);
        await _refreshTokenRepository.DeactivateAllTokensAsync(user.Id);

        return await Handle(user);
    }
}
