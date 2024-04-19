namespace Base.Core.Options;

public class PasswordOptions
{
    /// <summary>
    /// Соль для шифрования пароля
    /// </summary>
    public string Salt { get; set; } = string.Empty;
}
