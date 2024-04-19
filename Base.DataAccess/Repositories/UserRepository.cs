using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Base.Core;
using Base.UseCases.Abstractions;
using Base.DataAccess.Dto;
using Base.DataAccess;

namespace MNX.SecurityManagement.DataAccess.Repositories;

/// <summary>
/// Реализация <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    private readonly DataBaseContext _context;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    public UserRepository(DataBaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async Task<AuthUser?> GetByIdAsync(long id)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<AuthUser?>(entity);
    }

    /// <inheritdoc/>
    public async Task<AuthUser?> Resolve(string login)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login)
            .ConfigureAwait(false);

        return _mapper.Map<AuthUser?>(entity);
    }

    /// <inheritdoc/>
    public async Task<AuthUser?> Resolve(string login, string password)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login && x.PasswordHash == password)
            .ConfigureAwait(false);

        return _mapper.Map<AuthUser?>(entity);
    }

    /// <inheritdoc/>
    public async Task<AuthUser> CreateAsync(AuthUser user)
    {
        var entity = _mapper.Map<UserDto>(user);
        await _context.Users.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return _mapper.Map<AuthUser>(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(AuthUser user)
    {
        var entity = _mapper.Map<UserDto>(user);
        _context.Users.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
