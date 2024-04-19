using Base.Core;

namespace Base.UseCases.Abstractions;

public interface ITokenRepository
{
    /// <summary>
    /// Создать токен
    /// </summary>
    /// <param name="refreshToken"> Токен </param>
    /// <returns> Токен </returns>
    Task<string?> CreateAsync(RefreshToken refreshToken);

    /// <summary>
    /// Получить список токенов по идентификатору пользователя
    /// </summary>
    /// <param name="userId"> Идентификуатор пользователя </param>
    /// <returns> Список токенов </returns>
    IAsyncEnumerable<RefreshToken> GetByUserIdAsync(long userId);

    /// <summary>
    /// Пометить токены использованным
    /// </summary>
    /// <param name="token"> Токен </param>
    /// <returns> <see langword="true"/>, если токен был успешно использован (и ранее не был использован), иначе <see langword="false"/> </returns>
    Task<bool> UseByTokenAsync(string token);

    /// <summary>
    /// Получить сущность токена по токену
    /// </summary>
    /// <param name="token"> Рефреш токен </param>
    /// <returns> Сущность токена </returns>
    Task<RefreshToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Пометить все токены пользователя использованными
    /// </summary>
    /// <param name="userId"> Идентияикатор пользователя </param>
    Task UseAllByUserIdAsync(long userId);
}
