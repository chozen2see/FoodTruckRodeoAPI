using System.Collections.Generic;

namespace FoodTruckRodeo.API.DTOs
{
  public class FoodTruckForUsersDTO
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Tagline { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public bool IsActiveFoodTruck { get; set; }

    public int UserId { get; set; }

    public ICollection<UserForListDTO> Users { get; set; }
  }
}