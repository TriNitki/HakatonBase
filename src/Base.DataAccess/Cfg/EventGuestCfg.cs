using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Cfg;

internal class EventGuestCfg : IEntityTypeConfiguration<EventGuest>
{
    public void Configure(EntityTypeBuilder<EventGuest> builder)
    {
        builder.HasKey(x => new { x.UserId, x.EventId });

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Event)
            .WithMany()
            .HasForeignKey(x => x.EventId);
    }
}
