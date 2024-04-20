using Base.Contracts.Event;
using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Queries.Events.GetEventsForModeration;

public class GetEventsForModerationQuery : IRequest<Result<List<ReducedEventDto>>>
{

}
