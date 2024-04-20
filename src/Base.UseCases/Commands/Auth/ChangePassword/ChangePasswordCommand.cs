using MediatR;
using Pkg.UseCases;
using Base.Contracts;

namespace Base.UseCases.Commands.Auth.ChangePassword;

/// <summary>
/// Команда на изменение пароля.
/// </summary>
public class ChangePasswordCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Новый пароль.
    /// </summary>
    public string NewPassword { get; }

    public ChangePasswordCommand(string login, string password, string newPassword)
    {
        Login = login;
        Password = password;
        NewPassword = newPassword;
    }
}
