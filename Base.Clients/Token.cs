namespace Base.Clients;

/// <summary>
/// Токен авторизации.
/// </summary>
internal class Token
{
    /// <summary>
    /// Время истечения срока годности.
    /// </summary>
    private readonly DateTime _expiration;

    /// <summary>
    /// Признак валидности токена.
    /// </summary>
    private bool _isValid;

    /// <summary>
    /// Значение токена.
    /// </summary>
    internal string Value { get; }

    /// <summary>
    /// Создаёт экземпляр класса <see cref="Token"/>.
    /// </summary>
    /// <param name="value"> Значение токена. </param>
    /// <param name="expiration"> Время истечения срока годности. </param>
    internal Token(string value, DateTime expiration)
    {
        Value = value;
        _expiration = expiration;
        _isValid = true;
    }

    /// <summary>
    /// Получить признак валидности токена.
    /// </summary>
    /// <returns></returns>
    internal bool IsValid()
    {
        return _isValid && _expiration < DateTime.UtcNow;
    }

    /// <summary>
    /// Сдеалть токен не валидным.
    /// </summary>
    internal void Invalidate() => _isValid = false;
}
