using Pkg.UseCases;
using Base.Contracts.Event;
using Base.UseCases.Queries.Events.FilterEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Base.UseCases.Commands.Events.CreateEvent;
using Base.UseCases.Queries.Events.GetEvent;
using Base.UseCases.Commands.Events.ModerateEvent;
using Base.UseCases.Queries.Events.GetEventsForModeration;
using Base.UseCases.Abstractions;

namespace Base.Service.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с событиями
/// </summary>
[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IEventRepository eventRepository;
    private readonly IAuthUserAccessor authUserAccessor;

    /// <inheritdoc/>
    public EventController(IMediator mediator, IEventRepository eventRepository, IAuthUserAccessor authUserAccessor)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.eventRepository = eventRepository;
        this.authUserAccessor = authUserAccessor;
    }

    /// <summary>
    /// Поиск ивентов по фильтру
    /// </summary>
    /// <param name="city"> Город </param>
    /// <param name="categories"> Категории </param>
    /// <param name="byPopularity"> По популярности </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReducedEventDto>), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> FilterEvents([FromQuery] string? city, [FromQuery] bool byPopularity = false, [FromQuery] string[]? categories = null)
    {
        var result = await _mediator.Send(new FilterEventsQuery()
        {
            City = city,
            ByPopularity = byPopularity,
            Categories = categories
        });
        return result.ToActionResult();
    }

    /// <summary>
    /// Получение полных данных о ивенте
    /// </summary>
    /// <param name="eventId"> Идентификатор ивента </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="404"> Ивент не был найден </response>
    [HttpGet("{eventId:Guid}")]
    [ProducesResponseType(typeof(EventDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 404)]
    public async Task<IActionResult> GetEvent(Guid eventId)
    {
        var result = await _mediator.Send(new GetEventQuery() { Id = eventId});
        return result.ToActionResult();
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
    public async Task<IActionResult> Create(CreateEventCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
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
        var result = await _mediator.Send(new GetEventsForModerationQuery());
        return result.ToActionResult();
    }

    /// <summary>
    /// Произвести модерацию ивента
    /// </summary>
    /// <response code="204"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="401"> Пользователь не авторизован </response>
    /// <response code="403"> Пользователь имеет недостаточно прав </response>
    [HttpPatch("moderation")]
    [Authorize]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 401)]
    [ProducesResponseType(typeof(List<string>), 403)]
    public async Task<IActionResult> ModerateEvent(ModerateEventCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }

    /// <summary>
    /// Отправить заявку на участие в ивенте
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("favorite")]
    [Authorize]
    public async Task<IActionResult> GoToEvent(GoToEventRequest request)
    {
        await eventRepository.GoToEvent(request.EventId, authUserAccessor.GetUserId());
        return Ok();
    }
    
}
