namespace Base.Contracts.Auth;

/// <summary>
/// Модель запроса на обновление пары токенов
/// </summary>
public class RefreshTokensRequest
{
    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; }
}
