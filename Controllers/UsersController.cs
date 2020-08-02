using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckRodeo.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  // ApiController attribute adds validation and allows Core/MVC to infer where data is coming from (body) for the parameters of the method
  public class UsersController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    public UsersController(IFoodTruckRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _repo.GetUsers();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
      var user = await _repo.GetUser(id);
      return Ok(user);
    }
  }
}