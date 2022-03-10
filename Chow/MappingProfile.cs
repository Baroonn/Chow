using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Chow;
public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Store, StoreReadDto>();
        CreateMap<MealComponent, MealComponentReadDto>();
        CreateMap<StoreCreateDto, Store>();
        CreateMap<MealComponentCreateDto, MealComponent>();
        CreateMap<MealComponentUpdateDto, MealComponent>();
        CreateMap<StoreUpdateDto, Store>();
    }
}