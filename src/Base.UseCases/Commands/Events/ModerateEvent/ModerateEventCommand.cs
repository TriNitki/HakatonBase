using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Commands.Events.ModerateEvent;

public class ModerateEventCommand : IRequest<Result<Unit>>
{
    public Guid EventId { get; set; }

    public bool IsApproved { get; set; }
 }
