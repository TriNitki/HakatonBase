using Base.Contracts.Auth;
using Base.Contracts.Event;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Base.Service.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с событиями
/// </summary>
[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public EventController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Поиск ивентов по фильтру
    /// </summary>
    /// <param name="city"> Город </param>
    /// <param name="categories"> Категории </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReducedEventDto>), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> FilterEvents([FromQuery] string? city, [FromQuery] string[] categories)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получение полных данных о ивенте
    /// </summary>
    /// <param name="eventId"> Идентификатор ивента </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="404"> Ивент не был найден </response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EventDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 404)]
    public async Task<IActionResult> GetEvent(int eventId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Создание события
    /// </summary>
    /// <response code="201"> Успешно создан </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="401"> Пользователь не авторизован </response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ReducedEventDto), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 404)]
    public async Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Установка статуса ивента
    /// </summary>
    /// <response code="201"> Успешно создан </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="401"> Пользователь не авторизован </response>
    /// <response code="403"> Пользователь имеет недостаточно прав </response>
    /// <response code="404"> Ивент не найдено </response>
    [HttpPatch("status")]
    [Authorize]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 401)]
    [ProducesResponseType(typeof(List<string>), 403)]
    [ProducesResponseType(typeof(List<string>), 404)]
    public async Task<IActionResult> SetStatus()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Изменить ивент
    /// </summary>
    /// <response code="204"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="401"> Пользователь не авторизован </response>
    /// <response code="403"> Пользователь имеет недостаточно прав </response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 401)]
    [ProducesResponseType(typeof(List<string>), 403)]
    public async Task<IActionResult> Update()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Проверка разрешения на изменение ивента
    /// </summary>
    /// <response code="200"> Успешно </response>
    /// <response code="401"> Пользователь не авторизован </response>
    [HttpGet("check-permission")]
    [Authorize]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(List<string>), 401)]
    public async Task<IActionResult> CheckUserPermission()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Список ивентов на модерацию
    /// </summary>
    /// <response code="200"> Успешно </response>
    /// <response code="401"> Пользователь не авторизован </response>
    /// <response code="403"> Пользователь имеет недостаточно прав </response>
    [ProducesResponseType(typeof(List<ReducedEventDto>), 200)]
    [ProducesResponseType(typeof(List<string>), 401)]
    [ProducesResponseType(typeof(List<string>), 403)]
    [HttpGet("moderation")]
    [Authorize]
    public async Task<IActionResult> GetEventsForModeration()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Произвести модерацию ивента
    /// </summary>
    /// <response code="204"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="401"> Пользователь не авторизован </response>
    /// <response code="403"> Пользователь имеет недостаточно прав </response>
    [HttpPost("moderation")]
    [Authorize]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 401)]
    [ProducesResponseType(typeof(List<string>), 403)]
    public async Task<IActionResult> ModerateEvent()
    {
        throw new NotImplementedException();
    }
}
