using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Cfg;

internal class EventToUserCfg : IEntityTypeConfiguration<EventToUser>
{
    public void Configure(EntityTypeBuilder<EventToUser> builder)
    {
        builder.HasKey(x => new { x.EventId, x.UserId });
    }
}
