namespace Base.Contracts.Event;

public class EventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartDT { get; set; }

    public List<string>? Categories { get; set; }
}
