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

    
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string Name { get; set; }

[Required]
    [RegularExpression("^(.+)@(.+)$")]
    public string Email { get; set; }

    [StringLength(10, MinimumLength = 0, ErrorMessage = "Phone must be 10 characters with no special charaters.")]
    public string PhoneNumber { get; set; }
  }
}