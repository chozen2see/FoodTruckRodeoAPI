using System.Threading.Tasks;
using Data;
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
    public MenusController(IFoodTruckRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenus()
    {
        var menus = await _repo.GetMenus();
        return Ok(menus);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(int id) 
    {
        var menu = await _repo.GetMenu(id);
        return Ok(menu);
    }
  }
}