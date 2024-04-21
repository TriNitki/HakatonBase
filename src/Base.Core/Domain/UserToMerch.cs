namespace Base.Core.Domain;

public class UserToMerch
{
    public long UserId { get; set; }
    public Guid MerchId { get; set; }
    public uint Amount { get; set; }
    public DateTime PurchasedAt { get; set; }

    public User User { get; set; }
    public Merch Merch { get; set; }
}
