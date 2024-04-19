namespace Base.Core;

/// <summary>
/// Токен обновления.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Токен.
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Время истечения срока действия.
    /// </summary>
    public DateTime Expiration { get; }

    /// <summary>
    /// Признак использованного токена.
    /// </summary>
    public bool IsUsed { get; private set; }

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public long UserId { get; }

    public RefreshToken(string token, DateTime expiration, long userId)
    {
        Token = token;
        Expiration = expiration;
        UserId = userId;
    }

    public RefreshToken(string token, DateTime expiration, bool isUsed,  long userId)
        : this(token, expiration, userId)
    {
        IsUsed = isUsed;
    }

    /// <summary>
    /// Получить признак валидности токена.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>, если токен валидный, иначе <see langword="false"/>.
    /// </returns>
    public bool IsValid()
    {
        return !IsUsed && Expiration > DateTime.UtcNow;
    }

    /// <summary>
    /// Деактивировать токен.
    /// </summary>
    public void Deactivate()
    {
        IsUsed = true;
    }
}
