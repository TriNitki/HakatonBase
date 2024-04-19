using Base.Core;

namespace Base.Core.Providers;

/// <summary>
/// Предоставляет взаимодействия с JWT токенами
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Генерация Access токена
    /// </summary>
    /// <param name="userId"> Уникальный идентификатор пользователя </param>
    /// <param name="nikname"> Псевдоним пользователя </param>
    /// <returns> Access токен </returns>
    string GenerateAccess(long userId, string nikname);

    /// <summary>
    /// Генерация и сохрание токена в БД
    /// </summary>
    /// <param name="userId"> Уникальный идентификатор пользователя </param>
    /// <returns> Refresh токен </returns>
    Task<RefreshToken> GenerateSaveRefreshAsync(long userId);

    /// <summary>
    /// Помечает токен использованным в БД
    /// </summary>
    /// <param name="token"> Токен </param>
    Task UseRefreshByTokenAsync(string token);

    /// <summary>
    /// Получает объект токена из базы данных по токену
    /// </summary>
    /// <returns> Объект токена </returns>
    Task<RefreshToken?> GetRefreshByToken(string token); 

    /// <summary>
    /// Проверка токена на валидность
    /// </summary>
    /// <param name="token"> Токен </param>
    /// <returns> Состояние валидности токена </returns>
    bool Validate(string token);
}
