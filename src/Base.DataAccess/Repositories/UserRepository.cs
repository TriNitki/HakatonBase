using Microsoft.EntityFrameworkCore;
using Base.UseCases.Abstractions;
using Base.Core.Domain;

namespace Base.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _context;

    public UserRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<long> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return user.Id;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Login == login)
            .ConfigureAwait(false);
    }

    public async Task<User?> GetByLoginPasswordAsync(string login, string password)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Login == login && x.PasswordHash == password)
            .ConfigureAwait(false);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
