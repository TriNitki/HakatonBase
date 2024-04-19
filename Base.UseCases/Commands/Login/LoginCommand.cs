using MediatR;
using UseCases;
using Base.Contracts;

namespace Base.UseCases.Commands.Login;

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
