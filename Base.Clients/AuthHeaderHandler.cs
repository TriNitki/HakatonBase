using System.Net.Http.Headers;

namespace Base.Clients;

/// <summary>
/// Обработчик заголовка аутентификации.
/// </summary>
public class AuthHeaderHandler : DelegatingHandler
{
    /// <summary>
    /// Схема аутентификации.
    /// </summary>
    private const string _schema = "Bearer";

    /// <summary>
    /// Провайдер токена авторизации для системного пользователя.
    /// </summary>
    private readonly ISystemUserTokenProvider _tokenProvider;

    /// <summary>
    /// Клиент авторизации.
    /// </summary>
    private readonly IAuthorizationClient _authClient;

    public AuthHeaderHandler(ISystemUserTokenProvider tokenProvider,
                             IAuthorizationClient authClient)
    {
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        _authClient = authClient ?? throw new ArgumentNullException(nameof(authClient));
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken)
    {
        var accessToken = await _tokenProvider.GetAccessTokenAsync(_authClient);
        request.Headers.Authorization = new AuthenticationHeaderValue(_schema, accessToken);
        var result = await base.SendAsync(request, cancellationToken);

        if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            accessToken = await _tokenProvider.UpdateTokensAsync(_authClient);
            request.Headers.Authorization = new AuthenticationHeaderValue(_schema, accessToken);
            result = await base.SendAsync(request, cancellationToken);
        }

        return result;
    }
}
