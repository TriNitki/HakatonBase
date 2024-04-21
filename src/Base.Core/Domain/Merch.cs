namespace Base.Core.Domain;

public class Merch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image {  get; set; }
    public uint Stock { get; set; }
    public double Price { get; set; }
}
