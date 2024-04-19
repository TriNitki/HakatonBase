namespace Base.Integration;

/// <summary>
/// Переменные среды аутентификации.
/// </summary>
internal static class AuthEnvironmentVariables
{
    /// <summary>
    /// Url адрес Fabio.
    /// </summary>
    internal static string FabioUrl { get; } = nameof(FabioUrl);

    /// <summary>
    /// Название сервиса безопасности.
    /// </summary>
    internal static string SecurityServiceName { get; } = nameof(SecurityServiceName);
}
