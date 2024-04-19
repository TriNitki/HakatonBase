using Base.Core;

namespace Base.UseCases.Abstractions;

/// <summary>
/// Репозиторий для доступа к пользователям.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <param name="id"> Уникальный идентификатор </param>
    /// <returns> Пользователь </returns>
    Task<AuthUser?> GetByIdAsync(long id);

    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <param name="login"> Логин. </param>
    /// <returns> Пользователь. </returns>
    Task<AuthUser?> Resolve(string login);

    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <param name="login"> Логин. </param>
    /// <param name="password"> Пароль. </param>
    /// <returns> Пользователь. </returns>
    Task<AuthUser?> Resolve(string login, string password);

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="user"> Пользователь </param>
    /// <returns> Модель пользователя. </returns>
    Task<AuthUser> CreateAsync(AuthUser user);

    /// <summary>
    /// Обновить пользователя по идентификатору
    /// </summary>
    /// <param name="user"> Пользователь </param>
    Task UpdateAsync(AuthUser user);
}
