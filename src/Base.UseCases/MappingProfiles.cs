using AutoMapper;
using Base.Core;
using Base.Core.Providers;
using Base.UseCases.Commands.Registration;

namespace Base.UseCases;

public class MappingProfiles : Profile
{
    public MappingProfiles(IPasswordHashProvider passwordHashProvider)
    {
        CreateMap<RegistrationCommand, User>()
            .ForMember(x => x.Nickname, opt => opt.MapFrom(x => x.Login))
            .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => passwordHashProvider.Encrypt(x.Password)));
    }
}
