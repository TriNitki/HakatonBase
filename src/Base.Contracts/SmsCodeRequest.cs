namespace Base.Contracts;

/// <summary>
/// Модель логина по смс коду
/// </summary>
public class SmsCodeRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Код из смс
    /// </summary>
    public string Code { get; set; }
}
