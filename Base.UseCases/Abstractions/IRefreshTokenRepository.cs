using Base.Core;

namespace Base.UseCases.Abstractions;

/// <summary>
/// Репозиторий для доступа к токенам обновления.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Получить сущность токена по токену
    /// </summary>
    /// <param name="token"> Токен обновления </param>
    /// <returns> Сущность токена </returns>
    Task<RefreshToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Создать токен
    /// </summary>
    /// <param name="refreshToken"> Токен </param>
    Task CreateAsync(RefreshToken refreshToken);

    /// <summary>
    /// Деактивировать токен.
    /// </summary>
    /// <param name="token"> Модель токена обновления. </param>
    Task DeactivateAsync(RefreshToken token);

    /// <summary>
    /// Пометить все токены пользователя использованными
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя </param>
    Task DeactivateAllTokensAsync(long userId);
}
