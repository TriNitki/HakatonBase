using MediatR;
using Pkg.UseCases;

namespace Base.UseCases.Commands.Events.CreateEvent;

public class CreateEventCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public double? Cost { get; set; }

    public List<string>? Categories { get; set; }
}
