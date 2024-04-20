using Base.Contracts.Event;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Queries.Events.FilterEvents;

public class FilterEventsQuery : IRequest<Result<List<ReducedEventDto>>>
{
    public string? City { get; set; }

    public bool ByPopularity { get; set; } = false;

    public string[]? Categories { get; set; }
}
