using AutoMapper;
using Base.Contracts.Event;
using Base.Core.Domain;

namespace MNX.SecurityManagement.DataAccess;

/// <summary>
/// Конфигурация маппера.
/// </summary>
public class DbMappingProfile : Profile
{
    public DbMappingProfile()
    {
        CreateMap<Event, ReducedEventDto>()
            .ConstructUsing(x => new ReducedEventDto()
            {
                Id = x.Id,
                Name = x.Name,
                City = x.City,
                StartAt = x.StartAt
            });
    }
}
