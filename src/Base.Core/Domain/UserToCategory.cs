namespace Base.Core.Domain;

public class UserToCategory
{
    public long UserId { get; set; }
    public string CategoryName { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }
}
