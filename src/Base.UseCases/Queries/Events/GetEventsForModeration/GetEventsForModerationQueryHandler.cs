using Base.Contracts.Event;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Events.CreateEvent;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Queries.Events.GetEventsForModeration;

public class GetEventsForModerationQueryHandler : IRequestHandler<GetEventsForModerationQuery, Result<List<ReducedEventDto>>>
{
    private readonly IEventRepository eventRepository;

    public GetEventsForModerationQueryHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public async Task<Result<List<ReducedEventDto>>> Handle(GetEventsForModerationQuery request, CancellationToken cancellationToken)
    {
        var events = await eventRepository.GetMaderationList();
        List<ReducedEventDto> result = new();

        foreach (var x in events)
        {
            result.Add(new ReducedEventDto()
            {
                City = x.City,
                Name = x.Name,
                Id = x.Id,
                StartAt = x.StartAt,
            });
        }
        return Result<List<ReducedEventDto>>.Success(result);
    }
}
