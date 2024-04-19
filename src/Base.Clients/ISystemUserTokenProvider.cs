namespace Base.Clients;

/// <summary>
/// Провайдер токена авторизации для системного пользователя.
/// </summary>
public interface ISystemUserTokenProvider
{
    /// <summary>
    /// Получить токен доступа.
    /// </summary>
    /// <param name="authClient"> Клиент для авторизации. </param>
    /// <returns> Токен доступа. </returns>
    public Task<string> GetAccessTokenAsync(IAuthorizationClient authClient);

    /// <summary>
    /// Обновить пару токенов.
    /// </summary>
    /// <param name="authClient"> Клиент для авторизации. </param>
    /// <returns> Токен доступа. </returns>
    public Task<string> UpdateTokensAsync(IAuthorizationClient authClient);
}
