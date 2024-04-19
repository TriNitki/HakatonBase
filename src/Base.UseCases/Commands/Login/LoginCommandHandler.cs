using MediatR;
using Microsoft.Extensions.Options;
using Pkg.UseCases;
using Base.Contracts;
using Base.Core.Options;
using Base.Core.Services;
using Base.UseCases.Abstractions;

namespace Base.UseCases.Commands.Login;

/// <summary>
/// Обработчик команды логина.
/// </summary>
public class LoginCommandHandler : LoginBaseHandler, IRequestHandler<LoginCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;
    private readonly string _salt;

    public LoginCommandHandler(
        IUserRepository userRepository,
        ITokenService tokenService,
        IOptions<PasswordOptions> passwordOptions) : base(tokenService)
    {
        _userRepository = userRepository;
        _salt = passwordOptions.Value.Salt ?? throw new ArgumentNullException(nameof(passwordOptions));
    }

    public async Task<Result<Tokens>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Resolve(
            request.Login,
            CryptographyService.HashPassword(request.Password, _salt));

        if (user is null)
        {
            return Result<Tokens>.Invalid("Invalid password or login");
        }

        if (user.IsBlocked)
        {
            // TODO: возвращать 401 статус код. Сделать кастомный результат логина.
            return Result<Tokens>.Invalid("User is blocked");
        }

        return await Handle(user);
    }
}
