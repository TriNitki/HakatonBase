namespace Base.DataAccess.Dto;

/// <summary>
/// Токен обновления.
/// </summary>
public class RefreshTokenDto
{
    /// <summary>
    /// Токен.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Время истечения срока действия.
    /// </summary>
    public DateTime Expiration { get; set; }

    /// <summary>
    /// Признак использованного токена.
    /// </summary>
    public bool IsUsed { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public long UserId { get; set; }
}
