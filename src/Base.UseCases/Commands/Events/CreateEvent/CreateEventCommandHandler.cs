using AutoMapper;
using Base.Core.Domain;
using Base.UseCases.Abstractions;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Commands.Events.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<Guid>>
{
    private readonly IEventRepository eventRepository;
    private readonly IMapper mapper;
    private readonly IAuthUserAccessor authUserAccessor;

    public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IAuthUserAccessor authUserAccessor)
    {
        this.eventRepository = eventRepository;
        this.mapper = mapper;
        this.authUserAccessor = authUserAccessor;
    }

    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = mapper.Map<Event>(request);
        newEvent.CreatorId = authUserAccessor.GetUserId();
        await eventRepository.Create(newEvent);

        return Result<Guid>.SuccessfullyCreated(newEvent.Id);
    }
}
