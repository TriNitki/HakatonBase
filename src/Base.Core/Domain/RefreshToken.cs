namespace Base.Core.Domain;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; } = false;
    public long UserId { get; set; }
}
