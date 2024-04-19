namespace Base.Contracts.Event;

public class SetEventStatusRequest
{
    public Guid EventId { get; set; }

    public bool IsActive { get; set; }
}
