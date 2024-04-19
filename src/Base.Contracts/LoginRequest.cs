namespace Base.Contracts;

/// <summary>
/// Модель запроса на логин.
/// </summary>
/// <param name="Login"> Логин. </param>
/// <param name="Password"> Пароль. </param>
public record LoginRequest(string Login, string Password);
