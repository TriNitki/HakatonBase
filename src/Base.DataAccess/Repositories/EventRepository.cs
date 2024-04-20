using Base.Core.Domain;
using Base.UseCases.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DataBaseContext _context;

    public EventRepository(DataBaseContext context)
    {
        _context = context;
    }


    public async Task<Guid?> Create(Event newEvent, List<string>? categories)
    {
        await _context.Events.AddAsync(newEvent).ConfigureAwait(false);
        await _context.EventToCategory.AddRangeAsync(categories.Select(x => new EventToCategory() { CategoryName = x, EventId = newEvent.Id }));
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return newEvent.Id;
    }

    public async Task<List<Event>?> Filter(string? city, List<string>? categories, bool byPopularity = false)
    {
        var events = _context.Events.AsQueryable();

        events = events.Where(e => e.IsModerated)
            .Where(e => e.StartAt > DateTime.UtcNow)
            .OrderBy(e => e.StartAt);

        if (!string.IsNullOrEmpty(city))
        {
            events = events.Where(e => e.City == city);
        }

        if (byPopularity)
        {
            events = events.OrderByDescending(e => e.EventToUser.Count);
        }

        if (categories != null)
        {
            foreach (var category in categories)
            {
                events = events.Where(e => e.EventToCategory.Any(x => x.CategoryName == category));
            }
        }

        return await events.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    public async Task<Event?> GetById(Guid id)
    {
        return await _context.Events
            .Include("Creator").Include("EventToCategory")
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
    }

    public async Task<List<Event>?> GetMaderationList()
    {
        return await _context.Events
            .AsNoTracking()
            .Where(x => !x.IsModerated)
            .OrderByDescending(x => x.StartAt)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task GoToEvent(Guid id, long userId)
    {
        await _context.EventToUser.AddAsync(new EventToUser() { EventId = id, UserId = userId}).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task ProcessEvent(Guid id, bool isApproved)
    {
        var eventDomain = await _context.Events.SingleOrDefaultAsync(x => x.Id == id);
        eventDomain.IsModerated = true;
        eventDomain.IsActive = isApproved;
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
