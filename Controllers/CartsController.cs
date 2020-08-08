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

    // GET ENTIRE CART
    [HttpGet("{id}/foodtruck/{foodTruckId}/user/{userId}")]
    public async Task<IActionResult> GetCart(int foodTruckId, int userId, int id)
    {
      var cart = await _repo.GetCart(foodTruckId, userId, id);

      var cartToReturn = _mapper.Map<CartDTO>(cart);

      return Ok(cartToReturn);
    }

    // COMPLETE PURCHASE
    [HttpPut("{id}/cp")]
    public async Task<IActionResult> CompletePurchase(int id)
    {
      var order = await _repo.CompletePurchase(id);

      var orderToReturn = _mapper.Map<CartDTO>(order);

      return Ok(orderToReturn);
    }

    // ADD ITEM TO CART
    [HttpPost("{id}/foodtruck/{foodTruckId}/user/{userId}/item/{itemId}/qty/{qty}")]
    public async Task<IActionResult> AddCartItem(int id, int foodTruckId, int userId, int itemId, int qty)
    {
      var item = await _repo.AddCartItem(id, foodTruckId, userId, itemId, qty);

      var itemToReturn = _mapper.Map<ItemDetailsForCartDTO>(item);

      return Ok(itemToReturn);
    }

    // UPDATE ITEM QUANTITY
    [HttpPut("{id}/item/{itemId}/qty/{qty}")]
    public async Task<IActionResult> UpdateItem(int id, int itemId, int qty)
    {
      var item = await _repo.UpdateItem(id, itemId, qty);

      var itemToReturn = _mapper.Map<ItemDetailsForCartDTO>(item);

      return Ok(itemToReturn);
    }

    // REMOVE ITEM FROM CART
    [HttpDelete("{id}/foodtruck/{foodTruckId}/user/{userId}/item/{itemId}")]
    public async Task<IActionResult> DeleteItem(int id, int foodTruckId, int userId, int itemId)
    {
      var cart = await _repo.DeleteItem(id, foodTruckId, userId, itemId);

      var cartToReturn = _mapper.Map<CartDTO>(cart);

      return Ok(cartToReturn);
    }

        // REMOVE CART
    [HttpDelete("{id}/foodtruck/{foodTruckId}/user/{userId}")]
    public async Task<IActionResult> DeleteCart(int id, int foodTruckId, int userId)
    {
      var cart = await _repo.DeleteCart(id, foodTruckId, userId);

      var deletedCart = _mapper.Map<CartDTO>(cart);

      return Ok(deletedCart);
    }
  }
}