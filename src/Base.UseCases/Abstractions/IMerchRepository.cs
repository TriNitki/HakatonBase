using Base.Core.Domain;

namespace Base.UseCases.Abstractions;

public interface IMerchRepository
{
    Task<Guid> Create(Merch merch);
    Task Purchase(long userId, Guid merchId, uint amount);
    Task<List<Merch>?> GetAll();
}
