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
  // ApiController attribute adds validation and allows Core/MVC to infer where data is coming from (body) for the parameters of the method
  public class UsersController : ControllerBase
  {
    private readonly IFoodTruckRepository _repo;
    private readonly IMapper _mapper;
    public UsersController(IFoodTruckRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    // UserForListDTO userForListDTO
    {
      var users = await _repo.GetUsers();

      // use AutoMapper to map DTO <destination> to Model (source)
      var usersForListDTO = _mapper.Map<IEnumerable<UserForListDTO>>(users);

      return Ok(usersForListDTO);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
      var user = await _repo.GetUser(id);

      // Execute a mapping from the (source object) to a new <destination object> with supplied mapping options.
      var userToReturn = _mapper.Map<UserForListDTO>(user);

      return Ok(userToReturn);
    }

    // add update user route here
  }
}