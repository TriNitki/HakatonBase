using AutoMapper;
using Base.Core.Domain;
using Base.Core.Providers;
using Base.UseCases.Commands.Auth.Registration;
using Base.UseCases.Commands.Events.CreateEvent;

namespace Base.UseCases;

public class MappingProfiles : Profile
{
    public MappingProfiles(IPasswordHashProvider passwordHashProvider)
    {
        CreateMap<RegistrationCommand, User>()
            .ForMember(x => x.Nickname, opt => opt.MapFrom(x => x.Login))
            .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => passwordHashProvider.Encrypt(x.Password)));

        CreateMap<CreateEventCommand, Event>()
            .ConstructUsing(x => new Event()
            {
                Name = x.Name,
                Description = x.Description,
                Location = x.Location,
                City = x.City,
                StartAt = x.StartAt,
                PublishedAt = DateTime.UtcNow,
                Cost = x.Cost,
                Reward = x.Reward,
                IsActive = true,
                IsModerated = false
            });
    }
}
