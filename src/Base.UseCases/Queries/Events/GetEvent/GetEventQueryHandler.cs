using Base.Contracts.Event;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Events.CreateEvent;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Queries.Events.GetEvent;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, Result<EventDto>>
{
    private readonly IEventRepository eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public async Task<Result<EventDto>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var eventDto = await eventRepository.GetById(request.Id);

        List<string> cats = new();

        if (eventDto.EventToCategory != null)
        {
            foreach (var cat in eventDto.EventToCategory)
                cats.Add(cat.CategoryName);
        }


        return Result<EventDto>.Success(new EventDto()
        {
            Id = eventDto.Id,
            Name = eventDto.Name,
            Description = eventDto.Description,
            Location = eventDto.Location,
            City = eventDto.City,
            StartAt = eventDto.StartAt,
            Cost = eventDto.Cost,
            Reward = eventDto.Reward,
            Categories = cats,
            CreatorId = eventDto.CreatorId,
            CreatorNickname = eventDto.Creator.Nickname
        });
    }
}
