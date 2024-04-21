namespace Base.Contracts.Event;

public class EventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }

    public double? Cost { get; set; }

    public int Reward { get; set; }

    public long CreatorId { get; set; }

    public string CreatorNickname { get; set; } = String.Empty;

    public List<string>? Categories { get; set; }
}
