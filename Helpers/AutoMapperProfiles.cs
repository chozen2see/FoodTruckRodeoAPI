using System.Linq;
using AutoMapper;
using FoodTruckRodeo.API.DTOs;
using Models;

namespace FoodTruckRodeo.API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    // used to help understand source and destination being mapped
    public AutoMapperProfiles()
    {
      CreateMap<User, UserForListDTO>()
        .ForMember(
            dest => dest.FoodTruckId, 
            opt => opt.MapFrom(
                src => src.FoodTruckUsers
                .FirstOrDefault(
                    // if more food trucks are added will need to change this logic to isActive. Must return bool from lambda
                    ft => ft.FoodTruckId != 0
                ).FoodTruckId));
      CreateMap<Menu, MenuForListDTO>();
      CreateMap<Item, ItemsForMenuDTO>();
    }

  }
}