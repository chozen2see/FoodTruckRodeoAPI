using System.Collections.Generic;

namespace Models
{
  public class FoodTruckUser
  {
    public int Id { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsActiveFoodTruck { get; set; }

    // EF Core Relationships: 
    // FoodTruck (many) and User (many) Models 
    public int FoodTruckId { get; set; }
    public FoodTruck FoodTruck { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }


    // FoodTruckUser (one) and Cart (many) Models 
    public ICollection<Cart> Carts { get; set; }
  }
}