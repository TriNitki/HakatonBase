namespace Base.Core.Domain;

public class User
{
    public long Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool TwoFactorAuth { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
    public string? Email { get; set; }

    public List<EventToUser>? EventToUser { get; set; }
}
