using Microsoft.Extensions.Options;
using Base.Contracts;

namespace Base.Clients;

/// <summary>
/// Реализация <see cref="ISystemUserTokenProvider"/>.
/// </summary>
public class SystemUserTokenProvider : ISystemUserTokenProvider
{
    /// <summary>
    /// Токен доступа.
    /// </summary>
    private Token? _accessToken;

    /// <summary>
    /// Токен обновления.
    /// </summary>
    private Token? _refreshToken;

    /// <summary>
    /// Параметры jwt токенов.
    /// </summary>
    private readonly JwtTokensOptions _jwtOptions;

    /// <summary>
    /// Модель запроса на авторизацию.
    /// </summary>
    private readonly LoginRequest _loginRequest;

    /// <summary>
    /// Семафор.
    /// </summary>
    private readonly SemaphoreSlim _semaphore;

    public SystemUserTokenProvider(IOptions<JwtTokensOptions> jwtOptions,
                                   IOptions<SystemUserOptions> systemUserOptions)
    {
        var login = systemUserOptions.Value.Login;
        var password = systemUserOptions.Value.Password;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Не заданы логин или пароль для системного пользователя.");
        }

        _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        _loginRequest = new(login, password);
        _semaphore = new SemaphoreSlim(1);
    }

    /// <inheritdoc/>
    public async Task<string> GetAccessTokenAsync(IAuthorizationClient authClient)
    {
        if (!_accessToken?.IsValid() ?? false)
        {
            return _refreshToken?.IsValid() ?? false
                        ? await RefrashTokensAsync(authClient)
                        : await LoginAsync(authClient);
        }

        return _accessToken!.Value;
    }

    /// <inheritdoc/>
    public async Task<string> UpdateTokensAsync(IAuthorizationClient authClient)
    {
        return await LoginAsync(authClient);
    }

    /// <summary>
    /// Авторизироваться.
    /// </summary>
    /// <param name="authClient"> Клиент авторизации. </param>
    /// <returns> Токен доступа. </returns>
    private async Task<string> LoginAsync(IAuthorizationClient authClient)
    {
        await _semaphore.WaitAsync();

        try
        {
            if (!_accessToken?.IsValid() ?? false) // Получен в предыдущем семафоре?
            {
                SetTokens(await authClient.Login(_loginRequest));
            }
        }
        finally
        {
            _semaphore.Release();
        }

        return _accessToken!.Value;
    }

    /// <summary>
    /// Обновить пару токенов.
    /// </summary>
    /// <param name="authClient"> Клиент авторизации. </param>
    /// <returns> Токен доступа. </returns>
    private async Task<string> RefrashTokensAsync(IAuthorizationClient authClient)
    {
        await _semaphore.WaitAsync();

        try
        {
            if (!_accessToken?.IsValid() ?? false) // Получен в предыдущем семафоре?
            {
                SetTokens(await authClient.RefrashTokens(new RefreshTokensRequest(_refreshToken!.Value)));
            }
        }
        catch
        {
            SetTokens(await authClient.Login(_loginRequest));
        }
        finally
        {
            _semaphore.Release();
        }

        return _accessToken!.Value;
    }

    /// <summary>
    /// Задать пару токенов.
    /// </summary>
    /// <param name="tokens"> Пара токенов. </param>
    private void SetTokens(Tokens tokens)
    {
        _accessToken?.Invalidate();
        _refreshToken?.Invalidate();

        _accessToken = new Token(tokens.AccessToken,
                                 DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationInMinutes));

        _refreshToken = new Token(tokens.RefreshToken, tokens.RefreshExpiration.UtcDateTime);
    }
}
