using Pkg.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Base.UseCases.Commands.Login;
using Base.UseCases.Commands.ChangePassword;
using Base.UseCases.Commands.RefreshTokens;
using Base.UseCases.Commands.InvalidateRefreshToken;
using Base.UseCases.Commands.Registration;
using Base.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace MNX.SecurityManagement.Service.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с авторизацией пользователей
/// </summary>
[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="request"> Данные пользователя </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="409"> Пользователь с переданным логином уже существует </response>
    [HttpPost("registration")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 409)]
    public async Task<IActionResult> Registration(RegistrationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="request"> Данные пользователя </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(new LoginCommand(request.Login, request.Password));
        return result.ToActionResult();
    }

    /// <summary>
    /// Изменение пароля пользователя
    /// </summary>
    /// <param name="request"> JSON объект, содержащий логин/старый пароль/новый пароль </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPut("changePassword")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var result = await _mediator.Send(
            new ChangePasswordCommand(request.Login, request.Password, request.NewPassword));
        return result.ToActionResult();
    }

    /// <summary>
    /// Получить новую пару токенов по токену обновления
    /// </summary>
    /// <param name="request"> Запрос на обновление токенов авторизации. </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPost("refreshTokens")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> RefreshTokens(RefreshTokensRequest request)
    {
        var result = await _mediator.Send(new RefreshTokensCommand(request.RefreshToken));
        return result.ToActionResult();
    }

    /// <summary>
    /// Деактивация токена обновления.
    /// </summary>
    /// <param name="request"> Запрос на деактивацию токена обновления. </param>
    /// <response code="204"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPut("invalidateRefreshToken")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> InvalidateRefreshToken(InvalidateRefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }
}
