using MediatR;
using Pkg.UseCases;
using Base.UseCases.Abstractions;
using Base.Core.Providers;

namespace Base.UseCases.Commands.Auth.ChangePassword;

/// <summary>
/// Обработчик команды изменения пароля.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _refreshTokenRepository;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        ITokenRepository refreshTokenRepository,
        IPasswordHashProvider passwordHashProvider)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByLoginPasswordAsync(
            request.Login,
            _passwordHashProvider.Encrypt(request.Password));

        if (user == null)
            return Result<Unit>.Invalid("User wasn't found");

        user.PasswordHash = _passwordHashProvider.Encrypt(request.NewPassword);

        await _userRepository.UpdateAsync(user);

        await _refreshTokenRepository.UseAllByUserIdAsync(user.Id);
        return Result<Unit>.Empty();
    }
}
