using MediatR;
using Pkg.UseCases;
using Base.Contracts.Auth;

namespace Base.UseCases.Commands.Auth.Login;

/// <summary>
/// Команда логина.
/// </summary>
public class LoginCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; }

    public LoginCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
