namespace Base.Core.Domain;

public class EventToUser
{
    public Guid EventId { get; set; }
    public long UserId { get; set; }
    public bool IsActive { get; set; }

    public Event Event { get; set; }
    public User User { get; set; }
}
