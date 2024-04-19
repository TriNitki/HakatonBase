namespace Base.Contracts.Event;

public class ModerateEventRequest
{
    public Guid EventId { get; set; }

    public bool IsApproved { get; set; }
}
