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
        CreateMap<MealComponentUpdateDto, MealComponent>().ReverseMap();
        CreateMap<StoreUpdateDto, Store>().ReverseMap();
        CreateMap<Buyer, BuyerReadDto>();
        CreateMap<BuyerCreateDto, Buyer>();
        CreateMap<BuyerUpdateDto, Buyer>().ReverseMap();
        CreateMap<Order, OrderReadDto>()
            .ForCtorParam("CreatedAt", 
            opt => opt.MapFrom(x => x.CreatedAt.ToLocalTime()))
            .ForCtorParam("UpdatedAt",
            opt => opt.MapFrom(x => x.UpdatedAt.ToLocalTime()));
        CreateMap<OrderCreateDto, Order>();
        CreateMap<UserRegistrationDto, User>();
    }
}