using MediatR;
using Microsoft.Extensions.Options;
using UseCases;
using Base.Contracts;
using Base.Core;
using Base.Core.Options;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Login;

namespace Base.UseCases.Commands.Registration;

/// <summary>
/// Обработчик команды авторизации.
/// </summary>
public class RegistrationCommandHandler : LoginBaseHandler, IRequestHandler<RegistrationCommand, Result<Tokens>>
{
    /// <summary>
    /// Репозиторий для доступа к пользователям.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Параметры пароля.
    /// </summary>
    private readonly PasswordOptions _passwordOptions;

    /// <summary>
    /// Роли пользователя по умолчанию.
    /// </summary>
    private readonly string[] _defaultUserRoles;

    public RegistrationCommandHandler(
        IUserRepository userRepository,
        ITokenService tokenService,
        IOptions<PasswordOptions> passwordOptions,
        IOptions<List<string>> roles) : base(tokenService)
    {
        _userRepository = userRepository;
        _passwordOptions = passwordOptions.Value;
        _defaultUserRoles = roles.Value.ToArray() ?? throw new ArgumentNullException(nameof(roles));
    }

    public async Task<Result<Tokens>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.Resolve(request.Login) is not null)
        {
            return Result<Tokens>.Conflict("User with this login already exists");
        }

        var user = await _userRepository.CreateAsync(
                new AuthUser(request.Login, request.Password, _passwordOptions, _defaultUserRoles));

        return await Handle(user);
    }
}
