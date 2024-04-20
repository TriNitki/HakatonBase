using Base.UseCases.Abstractions;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Commands.Events.ModerateEvent;

public class ModerateEventCommandHandler : IRequestHandler<ModerateEventCommand, Result<Unit>>
{
    private readonly IEventRepository eventRepository;

    public ModerateEventCommandHandler(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public async Task<Result<Unit>> Handle(ModerateEventCommand request, CancellationToken cancellationToken)
    {
        await eventRepository.ProcessEvent(request.EventId, request.IsApproved);
        return Result<Unit>.Empty();
    }
}
