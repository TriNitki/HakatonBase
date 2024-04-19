using Base.Core.Options;
using Base.Core.Services;
using System.Security.Claims;

namespace Base.Core;

/// <summary>
/// Аутентифицированный пользователь.
/// </summary>
public class AuthUser
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; } = string.Empty;

    /// <summary>
    /// Хеш пароля.
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

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

    /// <summary>
    /// Роли.
    /// </summary>
    public string[] Roles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Конструктов для регистрации нового пользователя.
    /// </summary>
    /// <param name="login"> Логин </param>
    /// <param name="password"> Пароль </param>
    /// <param name="passwordOptions"> Параметры пароля. </param>
    public AuthUser(string login, string password, PasswordOptions passwordOptions, string[] roles)
    {
        Login = login;
        Nickname = login;
        SetPassword(password, passwordOptions);
        Roles = roles;
    }

    /// <summary>
    /// Конструктор для инициализации модели уже существующего пользователя.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <param name="nickname"> Имя пользователя. </param>
    /// <param name="login"> Логин. </param>
    /// <param name="password"> Пароль. </param>
    /// <param name="twoFactorAuth"> Признак включенной двухфакторной аутентификации. </param>
    /// <param name="isBlocked"> Признак заблокированного пользователя. </param>
    /// <param name="email"> Адрес электронной почты. </param>
    /// <param name="roles"> Список ролей. </param>
    public AuthUser(long id, string nickname, string login, string password,
                    bool twoFactorAuth, bool isBlocked, string? email, string[] roles)
    {
        Id = id;
        Nickname = nickname;
        Login = login;
        PasswordHash = password;
        TwoFactorAuth = twoFactorAuth;
        IsBlocked = isBlocked;
        Email = email;
        Roles = roles;
    }

    /// <summary>
    /// Задать пароль.
    /// </summary>
    /// <param name="password"> Пароль. </param>
    /// <param name="passwordOptions"> Параметры пароля. </param>
    public void SetPassword(string password, PasswordOptions passwordOptions)
    {
        PasswordHash = CryptographyService.HashPassword(password, passwordOptions.Salt);
    }

    /// <summary>
    /// Получить клэймы.
    /// </summary>
    /// <returns> Массив клэймов. </returns>
    public Claim[] GetClaims()
    {
        var claims = new List<Claim>(Roles.Length + 2)
        {
            new(ClaimTypes.NameIdentifier, Id.ToString()),
            new(ClaimTypes.Name, Nickname)
        };

        foreach (var role in Roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return claims.ToArray();
    }
}
