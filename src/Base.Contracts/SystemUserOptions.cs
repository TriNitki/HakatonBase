namespace Base.Contracts;

/// <summary>
/// Параметры системного пользователя.
/// </summary>
public class SystemUserOptions
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Роли.
    /// </summary>
    public string[] Roles { get; set; } = Array.Empty<string>();
}
