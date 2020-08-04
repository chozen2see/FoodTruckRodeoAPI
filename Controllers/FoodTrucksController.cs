using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using FoodTruckRodeo.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckRodeo.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class FoodTrucksController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    private readonly IMapper _mapper;
    public FoodTrucksController(IFoodTruckRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetFoodTrucks()
    {
      var foodTrucks = await _repo.GetFoodTrucks();

      var foodTrucksToReturn = _mapper.Map<IEnumerable<FoodTruckForListDTO>>(foodTrucks);

      return Ok(foodTrucksToReturn);

      // http://localhost:5000/api/menus/4/items/10
    }

    [HttpGet("{foodTruckId}")]
    public async Task<IActionResult> GetFoodTruck(int foodTruckId)
    {
      var foodTruck = await _repo.GetFoodTruck(foodTruckId);

      var foodTruckToReturn = _mapper.Map<FoodTruckForListDTO>(foodTruck);

      return Ok(foodTruckToReturn);
    }

    //  FOOD TRUCK MENUS
    [HttpGet("{foodTruckId}/menus")]
    public async Task<IActionResult> GetMenus(int foodTruckId)
    {
      var menus = await _repo.GetMenus(foodTruckId);

      // use AutoMapper to map DTO <destination> to Model (source)
      var menusToReturn = _mapper.Map<IEnumerable<MenuForListDTO>>(menus);

      return Ok(menusToReturn);
    }

    // FOOD TRUCK MENU ITEMS

    [HttpGet("{foodTruckId}/menus/{menuid}")]
    public async Task<IActionResult> GetMenu(int foodTruckId, int menuid)
    {
      var menu = await _repo.GetMenu(foodTruckId, menuid);

      var menuToReturn = _mapper.Map<MenuForListDTO>(menu);

      return Ok(menuToReturn);
    }

    // FOOD TRUCK MENU ITEM

    [HttpGet("{foodTruckId}/menus/{menuId}/items/{itemId}")]
    public async Task<IActionResult> GetItem(int foodTruckId, int menuId, int itemId)
    {
      var item = await _repo.GetItem(foodTruckId, menuId, itemId);

      var itemToReturn = _mapper.Map<ItemsForMenuDTO>(item);

      return Ok(itemToReturn);
    }

    [HttpGet("{foodTruckId}/menus/{menuId}/items")]
    public async Task<IActionResult> GetItems(int foodTruckId, int menuId, int itemId)
    {
      var items = await _repo.GetItems(foodTruckId, menuId);

      var itemsToReturn = _mapper.Map<IEnumerable<ItemsForMenuDTO>>(items);

      return Ok(itemsToReturn);
    }

    // FOOD TRUCK USERS

    // [HttpGet("{id}/users")]
    // public async Task<IActionResult> GetFoodTruckUsers()
    // // UserForListDTO userForListDTO
    // {
    //   var foodTruckUsers = await _repo.GetFoodTruckUsers();

    //   // use AutoMapper to map DTO <destination> to Model (source)
    //   var foodTruckUsersForListDTO = _mapper.Map<IEnumerable<FoodTruckForUsersDTO>>(foodTruckUsers);

    //   return Ok(foodTruckUsersForListDTO);
    // }
  }
}