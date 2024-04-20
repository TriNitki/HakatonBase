namespace Base.Core.Domain;

public class Category
{
    public string Name { get; set; } = string.Empty;

    public List<EventToCategory>? EventToCategory { get; set; }
}
