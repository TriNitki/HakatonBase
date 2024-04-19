namespace Base.Contracts;

/// <summary>
/// Модель запроса на смену пароля.
/// </summary>
/// <param name="Login"> Логин. </param>
/// <param name="Password"> Пароль. </param>
/// <param name="NewPassword"> Новый пароль. </param>
public record ChangePasswordRequest(string Login, string Password, string NewPassword);
