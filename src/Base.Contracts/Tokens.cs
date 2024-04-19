namespace Base.Contracts;

/// <summary>
/// Модель содержащая токены авторизации.
/// </summary>
/// <param name="AccessToken"> Токен доступа. </param>
/// <param name="RefreshToken"> Токен обновления. </param>
/// <param name="RefreshExpiration"> Дата окончания времени жизни токена обновления. </param>
public sealed record Tokens(string AccessToken, string RefreshToken, DateTimeOffset RefreshExpiration);
