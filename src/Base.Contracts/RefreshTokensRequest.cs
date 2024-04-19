namespace Base.Contracts;

/// <summary>
/// Модель запроса на обновление токенов авторизации.
/// </summary>
/// <param name="RefreshToken"> Токен обновления. </param>
public record RefreshTokensRequest(string RefreshToken);
