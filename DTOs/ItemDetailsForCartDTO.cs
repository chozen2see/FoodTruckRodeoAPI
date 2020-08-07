using System.Collections.Generic;
using Models;

namespace FoodTruckRodeo.API.DTOs
{
  public class ItemDetailsForCartDTO
  {
    public int Id { get; set; }

    public int Quantity { get; set; }

    // EF Core Relationships: 
    // Cart (many) and Item (many) Models 
    public int CartId { get; set; }

    // public int ItemId { get; set; }

    // public string ItemName { get; set; }

    // public float ItemPrice { get; set; }

    // public string ItemSize { get; set; }

    public ItemForItemDetailsDTO Item { get; set; } 
  }
}