namespace Base.Contracts.Event;

public class ReducedEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartDT { get; set; }
}
