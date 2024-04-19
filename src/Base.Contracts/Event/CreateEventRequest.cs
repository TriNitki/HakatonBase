using Base.Contracts.Category;

namespace Base.Contracts.Event;

public class CreateEventRequest
{ 
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartDT { get; set; }

    public List<string>? Categories { get; set; }
}
