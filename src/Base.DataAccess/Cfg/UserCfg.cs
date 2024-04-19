using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.Core;

namespace Base.DataAccess.Cfg;

/// <summary>
/// Конфигурация для таблицы с пользователями.
/// </summary>
internal class UserCfg : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(x => x.Login);
    }
}
