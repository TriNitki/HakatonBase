using Base.Contracts;
using Refit;

namespace Base.Clients;

/// <summary>
/// Клиент для авторизации в SecurityManagement.
/// </summary>
public interface IAuthorizationClient
{
    /// <summary>
    /// Авторизоваться.
    /// </summary>
    /// <param name="request"> Модель запроса на логин. </param>
    /// <returns> Токены авторизации. </returns>
    [Get("/api/auth/logIn")]
    public Task<Tokens> Login(LoginRequest request);

    /// <summary>
    /// Обновить токены авторизации.
    /// </summary>
    /// <param name="request"> Модель запроса на обновление токенов авторизации. </param>
    /// <returns> Токены авторизации. </returns>
    [Get("/api/auth/refrashTokens")]
    public Task<Tokens> RefrashTokens(RefreshTokensRequest request);
}
