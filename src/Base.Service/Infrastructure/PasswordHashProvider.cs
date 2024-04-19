using Microsoft.Extensions.Options;
using Base.Core.Options;
using Base.Core.Providers;
using System.Security.Cryptography;
using System.Text;

namespace Base.Service.Infrastructure;

/// <inheritdoc/>
public class PasswordHashProvider : IPasswordHashProvider
{
    private readonly PasswordOptions _options;

    /// <inheritdoc/>
    public PasswordHashProvider(IOptions<PasswordOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc/>
    public string Encrypt(string password)
    {
        return GenerateHashedPassword(password);
    }

    /// <inheritdoc/>
    public bool Verify(string enteredPassword, string hashedPassword)
    {
        string enteredPasswordHash = GenerateHashedPassword(enteredPassword);
        return string.Equals(enteredPasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }

    private string GenerateHashedPassword(string password)
    {
        byte[] hash;

        var salt = Encoding.UTF8.GetBytes(_options.Salt);
        byte[] byteString = Encoding.UTF8.GetBytes(password + salt);
        hash = SHA256.HashData(byteString);

        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
