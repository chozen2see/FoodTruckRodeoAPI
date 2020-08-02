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
  public class MenusController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    private readonly IMapper _mapper;
    public MenusController(IFoodTruckRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenus()
    {
      var menus = await _repo.GetMenus();

      // use AutoMapper to map DTO <destination> to Model (source)
      var menusToReturn = _mapper.Map<IEnumerable<MenuForListDTO>>(menus);

      return Ok(menusToReturn);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(int id)
    {
      var menu = await _repo.GetMenu(id);

      var menuToReturn = _mapper.Map<MenuForListDTO>(menu);

      return Ok(menuToReturn);
    }
  }
}