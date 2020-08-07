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
  public class OrdersController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    private readonly IMapper _mapper;
    public OrdersController(IFoodTruckRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    // need get route here
    [HttpGet("{id}/foodtruck/{foodTruckId}/user/{userId}")]
    public async Task<IActionResult> GetOrder(int foodTruckId, int userId, int id)
    {
      var order = await _repo.GetOrder(foodTruckId, userId, id);

      var orderToReturn = _mapper.Map<CartDTO>(order);

      return Ok(orderToReturn);
    }

    [HttpGet("history/foodtruck/{foodTruckId}/user/{userId}")]
    public async Task<IActionResult> GetOrderHistory(int foodTruckId, int userId)
    {
      var orders = await _repo.GetOrderHistory(foodTruckId, userId);

      var ordersToReturn = _mapper.Map<IEnumerable<CartDTO>>(orders);

      return Ok(ordersToReturn);
    }

// FILL THE ORDER 
    [HttpPut("{id}/fill")]
    public async Task<IActionResult> FillOrder(int id)
    {
      var order = await _repo.FillOrder(id);

      var orderToReturn = _mapper.Map<CartDTO>(order);

      return Ok(orderToReturn);
    }
  }
}