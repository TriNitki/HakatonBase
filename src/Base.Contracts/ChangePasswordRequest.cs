namespace Base.Contracts;

/// <summary>
/// Модель запроса на смену пароля
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Текущий пароль пользователя
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Новый пароль пользователя
    /// </summary>
    public string NewPassword { get; set; }
}
