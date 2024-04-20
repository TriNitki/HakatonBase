using Base.Contracts.Event;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Queries.Events.GetEvent;

public class GetEventQuery : IRequest<Result<EventDto>>
{
    public Guid Id { get; set; }
}
