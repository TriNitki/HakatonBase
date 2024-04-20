using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Cfg;

internal class EventToCategoryCfg : IEntityTypeConfiguration<EventToCategory>
{
    public void Configure(EntityTypeBuilder<EventToCategory> builder)
    {
        builder.HasKey(x => new { x.EventId, x.CategoryName });
    }
}
