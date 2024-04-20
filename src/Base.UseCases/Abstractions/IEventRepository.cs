using Base.Core.Domain;

namespace Base.UseCases.Abstractions;

public interface IEventRepository
{
    Task<List<Event>?> Filter(string? city, List<string>? categories, bool byPopularity = false);

    Task<Event?> GetById(Guid id);

    Task<Guid?> Create(Event newEvent, List<string>? categories);

    Task<List<Event>?> GetMaderationList();

    Task ProcessEvent(Guid id, bool isApproved);

    Task GoToEvent(Guid id, long userId);
}
