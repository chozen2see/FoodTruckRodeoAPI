using System.ComponentModel.DataAnnotations;

namespace FoodTruckRodeo.API.DTOs
{
  public class UserForRegisterDTO
  {
    // add data annotations to validate 
    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters.")]
    public string Password { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string Name { get; set; }


    [RegularExpression("^(.+)@(.+)$")]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
  }
}