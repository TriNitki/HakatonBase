using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DataAccess.Cfg;

internal class UserToCategoryCfg : IEntityTypeConfiguration<UserToCategory>
{
    public void Configure(EntityTypeBuilder<UserToCategory> builder)
    {
        builder.HasKey(x => new { x.UserId, x.CategoryName });
    }
}
