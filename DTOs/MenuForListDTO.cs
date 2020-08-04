using System.Collections.Generic;
namespace FoodTruckRodeo.API.DTOs
{
  public class MenuForListDTO
    {
        
    public int Id { get; set; }

    public string Name { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    // EF Core Relationships: 
    // FoodTruck (one) and Menu (many) Models 
    public int FoodTruckId { get; set; }

    // Menu(one) and Item (many) Models 
    public ICollection<ItemsForMenuDTO> Items { get; set; }
    }
}