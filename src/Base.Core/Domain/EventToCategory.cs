namespace Base.Core.Domain;

public class EventToCategory
{
    public Guid EventId { get; set; }
    public string CategoryName { get; set; }

    public Event Event { get; set; }
    public Category Category { get; set; }
}
