using Microsoft.Extensions.Options;
using Base.Core.Options;

namespace Base.Service.Options;

/// <inheritdoc/>
public class SecurityOptionsSetup : IConfigureOptions<SecurityOptions>
{
    private const string SectionName = "SecurityOptions";
    private readonly IConfiguration _configuration;

    /// <inheritdoc/>
    public SecurityOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public void Configure(SecurityOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
