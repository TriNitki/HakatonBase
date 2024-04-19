using Base.UseCases.Abstractions;
using System.Security.Claims;

namespace Base.Service.Infrastructure;

/// <inheritdoc/>
public class AuthUserAccessor : IAuthUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    /// <inheritdoc/>
    public AuthUserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    public long GetUserId()
    {
        var user = GetHttpContextUser();

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Access token has not user Id");

        return long.Parse(userId);
    }

    /// <inheritdoc/>
    public string GetUserNikname()
    {
        var user = GetHttpContextUser();

        var userLogin = user.FindFirstValue(ClaimTypes.Name)
            ?? throw new UnauthorizedAccessException("Access token has not user Id");

        return userLogin;
    }

    private ClaimsPrincipal GetHttpContextUser()
    {
        var httpContext = _contextAccessor.HttpContext;

        if (httpContext == null)
            throw new InvalidOperationException("Http context wasn't founded");

        return httpContext.User;
    }
}
