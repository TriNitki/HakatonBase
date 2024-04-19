using AutoMapper;
using Base.DataAccess.Dto;
using Base.Core;

namespace MNX.SecurityManagement.DataAccess;

/// <summary>
/// Конфигурация маппера.
/// </summary>
public class DbMappingProfile : Profile
{
    public DbMappingProfile()
    {
        CreateMap<RefreshTokenDto, RefreshToken>()
            .ConstructUsing(x => new RefreshToken(x.Token, x.Expiration, x.IsUsed, x.UserId))
            .ReverseMap();

        CreateMap<UserDto, AuthUser>()
            .ConstructUsing(x => new AuthUser(
                x.Id,
                x.Nickname,
                x.Login,
                x.PasswordHash,
                x.TwoFactorAuth,
                x.IsBlocked,
                x.Email,
                Array.Empty<string>()));

        CreateMap<AuthUser, UserDto>()
            .ConstructUsing(x => new UserDto()
            {
                Id = x.Id,
                Nickname = x.Nickname,
                Login = x.Login,
                PasswordHash = x.PasswordHash,
                Email = x.Email,
                TwoFactorAuth = x.TwoFactorAuth,
                IsBlocked = x.IsBlocked
            });
    }
}
