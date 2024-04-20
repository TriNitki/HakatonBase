using MediatR;
using Pkg.UseCases;
using Base.Contracts.Auth;

namespace Base.UseCases.Commands.Auth.Registration;

/// <summary>
/// Команда регистрации.
/// </summary>
public class RegistrationCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; }

    public RegistrationCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
