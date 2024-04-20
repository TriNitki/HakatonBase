using Base.Core.Domain;

namespace Base.UseCases.Abstractions;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();

    Task<string> Create(Category category);

    Task Favorite(string name, long userId);
}
