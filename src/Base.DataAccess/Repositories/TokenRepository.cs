using Microsoft.EntityFrameworkCore;
using Base.UseCases.Abstractions;
using Base.Core.Domain;

namespace Base.DataAccess.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly DataBaseContext _context;

    public TokenRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<string?> CreateAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return refreshToken.Token;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public IAsyncEnumerable<RefreshToken> GetByUserIdAsync(long userId)
    {
        return _context.RefreshTokens
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .AsAsyncEnumerable();
    }

    public async Task UseAllByUserIdAsync(long userId)
    {
        var tokenDomains = await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ToListAsync();

        tokenDomains.ForEach(x => x.IsUsed = true);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<bool> UseByTokenAsync(string token)
    {
        var tokenDomain = await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == token)
            .ConfigureAwait(false);

        if (tokenDomain == null || tokenDomain.IsUsed)
            return false;
        
        tokenDomain.IsUsed = true;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}
