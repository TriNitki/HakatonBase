﻿using MediatR;
using UseCases;
using Base.Contracts;

namespace Base.UseCases.Commands.Registration;

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