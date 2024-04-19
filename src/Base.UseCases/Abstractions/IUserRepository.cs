using Base.Core;

namespace Base.UseCases.Abstractions;

public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <param name="id"> Уникальный идентификатор </param>
    /// <returns> Пользователь </returns>
    Task<User?> GetByIdAsync(long id);

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="user"> Пользователь </param>
    /// <returns> Уникальный идентификатор </returns>
    Task<long> CreateAsync(User user);

    /// <summary>
    /// Обновить пользователя по идентификатору
    /// </summary>
    /// <param name="user"> Пользователь </param>
    Task UpdateAsync(User user);

    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    /// <param name="login"> Логин пользователя </param>
    /// <returns> Пользователь </returns>
    Task<User?> GetByLoginAsync(string login);

    /// <summary>
    /// Получить пользователя по логину и паролю
    /// </summary>
    /// <param name="login"> Логин пользователя </param>
    /// <param name="password"> Пароль пользователя </param>
    /// <returns> Пользователь </returns>
    Task<User?> GetByLoginPasswordAsync(string login, string password);
}
