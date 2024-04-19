namespace Base.DataAccess.Dto;

/// <summary>
/// Пользователь.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Хеш пароля.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Признак подключенной двухфакторной аутентификации.
    /// </summary>
    public bool TwoFactorAuth { get; set; }

    /// <summary>
    /// Признак заблокированного пользователя.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string? Email { get; set; }
}
