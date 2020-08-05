using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data;
using FoodTruckRodeo.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace FoodTruckRodeo.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  // ApiController attribute adds validation and allows Core/MVC to infer where data is coming from (body) for the parameters of the method
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;
    public AuthController(
      IAuthRepository repo, IConfiguration config
    )
    {
      _config = config;
      _repo = repo;
    }

    [HttpPost("register/{foodTruckId}")]
    public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO, int foodTruckId)
    {
      // Data Transfer Object (DTO) used to map domain model (User class) into simpler objects that get returned or displayed by the view

      // validate request via [ApiController] and DTO (UserForRegisterDTO)

      // convert username to lowercase, but user can log in with either case
      userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

      // check to see if username already taken
      if (await _repo.UserExists(userForRegisterDTO.Username))
        return BadRequest("User name already exists.");

      // if not, create a user object
      var userToCreate = new User
      {
        Username = userForRegisterDTO.Username,
        Name = userForRegisterDTO.Name,
        Email = userForRegisterDTO.Email,
        PhoneNumber = userForRegisterDTO.PhoneNumber
      };

      // pass user object and password to register user
      var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password, foodTruckId);

      // return to client name of route to use for generating the URL to get newly created entity and object
      return StatusCode(201); // temp fix CreatedAtRoute

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
    {
      // make sure username / password match what is in the DB
      // convert username to lower case to login
      var userFromRepo = await _repo.Login(
        userForLoginDTO.Username.ToLower(), userForLoginDTO.Password
      );

      // if login not found return unauthorized 
      // don't let them know if username exists or not
      if (userFromRepo == null)
        return Unauthorized();

      // start to build out the token which contains two claims
      var claims = new[]
      {
        // user's id
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
        // user's username
        new Claim(ClaimTypes.Name, userFromRepo.Username)
      };

      // server must sign token before it comes back to ensure validity

      // Added AppSettings.Token in appsettings.json 
      // Ordinarily a long randomly generated key.
      // create security key
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

      // signing credentials 
      // use key and hash algo to encrypt key
      var creds = new SigningCredentials(
        key, SecurityAlgorithms.HmacSha512Signature
      );

      // security token discriptor contains
      // claims, expiry date for token, sign in creds
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1), // 24 hours
        SigningCredentials = creds
      };

      // will allow us to create token once descriptor passed in below
      var tokenHandler = new JwtSecurityTokenHandler();

      // this will contain JWT token to return to client
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new
      {
        // write token in response to send back to client
        token = tokenHandler.WriteToken(token)
      });

    }

  }
}