namespace Base.Core.Options;

public class SecurityOptions
{
    /// <summary>
    /// Сектретный ключ для генерации JWT токена 
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Время жизни Access токена в минутах
    /// </summary>
    public int AccessTokenLifetimeInMinutes { get; set; }

    /// <summary>
    /// Время жизни Refresh токена в минутах
    /// </summary>
    public int RefreshTokenLifetimeInMinutes { get; set; }
}
