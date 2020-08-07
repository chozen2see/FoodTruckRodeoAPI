using System.Collections.Generic;
using Models;

namespace FoodTruckRodeo.API.DTOs
{
  public class CartDTO
  {
    public int Id { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public float Total { get; set; }

    public bool IsPurchaseComplete { get; set; }

    public bool IsOrderFilled { get; set; }

    public int FoodTruckId { get; set; }
    
    public int UserId { get; set; }

    public ICollection<ItemDetailsForCartDTO> CartItemDetails { get; set; }
  }
}