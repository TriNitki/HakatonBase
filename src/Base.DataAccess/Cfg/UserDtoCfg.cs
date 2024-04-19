using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.DataAccess.Dto;

namespace Base.DataAccess.Cfg;

/// <summary>
/// Конфигурация для таблицы с пользователями.
/// </summary>
internal class UserDtoCfg : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.HasAlternateKey(x => x.Login);
    }
}
