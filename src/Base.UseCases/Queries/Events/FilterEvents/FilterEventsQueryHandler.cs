using AutoMapper;
using Base.Contracts.Event;
using Base.Core.Domain;
using Base.UseCases.Abstractions;
using MediatR;
using Pkg.UseCases;
using System.Collections.Generic;

namespace Base.UseCases.Queries.Events.FilterEvents;

public class FilterEventsQueryHandler : IRequestHandler<FilterEventsQuery, Result<List<ReducedEventDto>>>
{
    private readonly IEventRepository eventRepository;
    private readonly IMapper mapper;

    public FilterEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        this.eventRepository = eventRepository;
        this.mapper = mapper;
    }

    public async Task<Result<List<ReducedEventDto>>> Handle(FilterEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await eventRepository.Filter(request.City, new List<string>(request.Categories), request.ByPopularity);
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
