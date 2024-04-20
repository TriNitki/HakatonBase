using Microsoft.EntityFrameworkCore;
using Base.UseCases.Abstractions;
using Base.Core.Domain;
using Base.Contracts.Event;

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

    public async Task<List<UserCrossDto>?> Filter(long userId, bool byCrossedEvents = false)
    {
        var etu = _context.EventToUser.Include("User").Where(x => userId != x.UserId).ToList();

        var result = etu.GroupBy(x => x.UserId)
                .Select(x => new UserCrossDto
                {
                    Count = x.Count(),
                    Id = x.First().UserId,
                    Nickname = x.First().User.Nickname,
                }).OrderByDescending(x => x.Count).ToList();

        return result;
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

    public async Task<List<Category>?> GetFavoritedCategories(long userId)
    {
        var utc = await _context.UserToCategory.Include("Category").Where(x => x.UserId == userId).ToListAsync().ConfigureAwait(false);
        List<Category> result = utc.Select(x => x.Category).ToList();
        return result;
    }

    public async Task<List<Event>?> GetFavoritedEvents(long userId)
    {
        var etu = await _context.EventToUser.Include("Event")
            .Where(x => x.UserId == userId)
            .Where(x => x.Event.StartAt > DateTime.UtcNow)
            .ToListAsync()
            .ConfigureAwait(false);
        List<Event> result = etu.Select(x => x.Event).ToList();
        return result;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
