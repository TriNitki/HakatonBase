namespace Base.Contracts.Auth;

/// <summary>
/// Модель запроса на логин
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
}
