using System.Collections.Generic;
using Models;

namespace FoodTruckRodeo.API.DTOs
{
  public class FoodTruckForListDTO
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Tagline { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    // public bool IsActiveFoodTruck { get; set; }

    public ICollection<MenuForFoodTruckDTO> Menus { get; set; }

    // public ICollection<CalendarEvent> CalendarEvents { get; set; }

  }
}