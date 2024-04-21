namespace Base.Contracts.Merch;

public class PurchaseRequest
{
    public Guid MerchId { get; set; }
    public uint Amount { get; set; }
}
