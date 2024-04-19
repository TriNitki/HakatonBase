namespace Base.Contracts.Auth;

/// <summary>
/// Модель jwt токенов
/// </summary>
public class Tokens
{
    /// <summary>
    /// Access токен
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Дата истечения срока действия Refresh токена
    /// </summary>
    public DateTime RefreshExpiration { get; set; }
}
