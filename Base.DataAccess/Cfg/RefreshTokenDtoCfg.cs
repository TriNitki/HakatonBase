using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Base.DataAccess.Dto;

namespace Base.DataAccess.Cfg;

/// <summary>
/// Конфигурация таблицы с токенами обновления.
/// </summary>
internal class RefreshTokenDtoCfg : IEntityTypeConfiguration<RefreshTokenDto>
{
    public void Configure(EntityTypeBuilder<RefreshTokenDto> builder)
    {
        builder.HasKey(x => x.Token);

        builder.HasOne<UserDto>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
