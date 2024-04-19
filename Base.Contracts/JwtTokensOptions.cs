namespace Base.Contracts;

/// <summary>
/// Параметры jwt токенов.
/// </summary>
public class JwtTokensOptions
{
    /// <summary>
    /// Время жизни токена доступа в минутах.
    /// </summary>
    public int AccessTokenExpirationInMinutes { get; set; } = 5;
}
