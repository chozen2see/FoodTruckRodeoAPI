using System.Collections.Generic;

namespace Models
{
  public class FoodTruck
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Tagline { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    // EF Core Relationships: 

    // FoodTruck (many) and User (many) Models 
    public ICollection<FoodTruckUser> FoodTruckUsers { get; set; }

    // FoodTruck (one) and Menu (many) Models 
    public ICollection<Menu> Menus { get; set; }

    // FoodTruck (one) and CalendarEvent (many) Models 
    public ICollection<CalendarEvent> CalendarEvents { get; set; }

    // FoodTruck (one) and Contact (many) Models    
    public ICollection<ContactRequest> ContactRequests { get; set; }
  }
}