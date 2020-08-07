using System.Threading.Tasks;
using AutoMapper;
using Data;
using FoodTruckRodeo.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// name of folder controller is in 
namespace FoodTruckRodeo.API.Controllers
{
  // [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CartsController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    private readonly IMapper _mapper;
    public CartsController(IFoodTruckRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    // need get route here
    [HttpGet("{id}/foodtruck/{foodTruckId}/user/{userId}")]
    public async Task<IActionResult> GetCart(int foodTruckId, int userId, int id)
    {
      var cart = await _repo.GetCart(foodTruckId, userId, id);

      var cartToReturn = _mapper.Map<CartDTO>(cart);

      return Ok(cartToReturn);
    }
  }
}