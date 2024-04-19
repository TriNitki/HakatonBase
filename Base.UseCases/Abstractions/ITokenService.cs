using Base.Core;

namespace Base.UseCases.Abstractions;

/// <summary>
/// Сервис для работы с токенами.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерация токена доступа.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <returns> Access токен </returns>
    string GenerateAccessToken(AuthUser user);

    /// <summary>
    /// Генерация токена обновления.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Модель токена обновления. </returns>
    Task<RefreshToken> GenerateRefreshToken(long userId);

    /// <summary>
    /// Деактивировать токен обновления.
    /// </summary>
    /// <param name="token"> Идентификатор токена обновления. </param>
    /// <returns> Деактивированный токен обновления. </returns>
    Task<RefreshToken?> DeactivateRefreshToken(string token);
}
