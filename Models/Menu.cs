using System.Collections.Generic;

namespace Models
{
  public class Menu
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    // EF Core Relationships: 
    // FoodTruck (one) and Menu (many) Models 
    public int FoodTruckId { get; set; }
    public FoodTruck FoodTruck { get; set; }

    // Menu(one) and Item (many) Models 
    public ICollection<Item> Items { get; set; }
  }
}