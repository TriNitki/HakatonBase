namespace Base.Core.Domain;

public class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    public double? Cost { get; set; }

    public bool IsActive { get; set; }

    public bool IsModerated { get; set; }

    public long CreatorId { get; set; }

    public List<Category>? Categories { get; set; }
    public List<User>? Guests { get; set; }
    public User? Creator { get; set; }
}
