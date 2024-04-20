namespace Base.Core.Domain;

public class EventGuest
{
    public long UserId { get; set; }

    public Guid EventId { get; set; }

    public bool IsActive { get; set; }


    public User? User {  get; set; }
    public Event? Event { get; set; }

}
