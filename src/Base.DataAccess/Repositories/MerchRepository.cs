using Base.Core.Domain;
using Base.UseCases.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Base.DataAccess.Repositories;

public class MerchRepository : IMerchRepository
{
    private readonly DataBaseContext context;

    public MerchRepository(DataBaseContext context)
    {
        this.context = context;
    }

    public async Task<Guid> Create(Merch merch)
    {
        await context.Merch.AddAsync(merch).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return merch.Id;
    }

    public async Task<List<Merch>?> GetAll()
    {
        return await context.Merch.Where(x => x.Stock > 0).ToListAsync().ConfigureAwait(false);
    }

    public async Task Purchase(long userId, Guid merchId, uint amount)
    {
        var merch = await context.Merch.SingleOrDefaultAsync(x => x.Id == merchId);
        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId);

        if (user == null || merch == null)
            return;

        if (merch.Stock < amount)
            return;

        if (merch.Price * amount > user.Points) 
            return;

        merch.Stock -= amount;
        user.Points -= merch.Price * amount;
        await context.UserToMerch.AddAsync(new UserToMerch()
        {
            UserId = userId,
            MerchId = merchId,
            Amount = amount,
            PurchasedAt = DateTime.UtcNow
        }).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
}
