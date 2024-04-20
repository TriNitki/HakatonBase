using Base.Core.Domain;
using Base.UseCases.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataBaseContext context;

    public CategoryRepository(DataBaseContext context)
    {
        this.context = context;
    }


    public async Task<string> Create(Category category)
    {
        await context.Categories.AddAsync(category).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return category.Name;
    }

    public async Task Favorite(string name, long userId)
    {
        var cat = await context.Categories.SingleOrDefaultAsync(x => x.Name == name);

        if (cat != null) 
        {
            await context.UserToCategory.AddAsync(new UserToCategory() { CategoryName = name,  UserId = userId });
        }
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<List<Category>> GetAll()
    {
        return await context.Categories.ToListAsync().ConfigureAwait(false);
    }
}
