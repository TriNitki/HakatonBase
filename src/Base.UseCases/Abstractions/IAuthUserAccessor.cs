namespace Base.UseCases.Abstractions;

public interface IAuthUserAccessor
{
    /// <summary>
    /// Получить псевдоним пользователя из Access токена
    /// </summary>
    /// <returns> Псевдоним пользователя </returns>
    string GetUserNikname();

    /// <summary>
    /// Получить идентификатор пользователя
    /// </summary>
    /// <returns> Идентификатор пользователя </returns>
    long GetUserId();
}
