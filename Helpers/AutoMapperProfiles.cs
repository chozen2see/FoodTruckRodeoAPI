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
                  ft => ft.IsActiveFoodTruck
              ).FoodTruckId))
        .ForMember(
          dest => dest.IsAdmin,
          opt => opt.MapFrom(
            src => src.FoodTruckUsers
            .FirstOrDefault(ft => ft.IsActiveFoodTruck).IsAdmin
          )
        );
      CreateMap<FoodTruck, FoodTruckForListDTO>();
      CreateMap<Menu, MenuForFoodTruckDTO>();
      CreateMap<Menu, MenuForListDTO>();
      CreateMap<Item, ItemsForMenuDTO>();
      // CreateMap<FoodTruck, FoodTruckForUsersDTO>()
        // .ForMember(
        //   dest => dest.UserId,
        //   opt => opt.MapFrom(
        //     src => src.FoodTruckUsers
        //     .FirstOrDefault(ft => ft.FoodTruckId == 1).UserId
        //   )
        // )
      // ;
    }

  }
}