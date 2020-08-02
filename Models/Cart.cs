using System.Collections.Generic;

namespace Models
{
  public class Cart
  {
    public int Id { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public float Total { get; set; }

    public bool IsPurchaseComplete { get; set; }

    public bool IsOrderFilled { get; set; }

    // EF Core Relationships: 
    // FoodTruckUser (one) and Cart (many) Models 
    public int FoodTruckUserId { get; set; }
    // public int FoodTruckId { get; set; }
    // public int UserId { get; set; }
    public FoodTruckUser FoodTruckUser { get; set; }

    // Cart (many) and Item (many) Models 
    public ICollection<CartItemDetail> CartItemDetails { get; set; }

  }
}