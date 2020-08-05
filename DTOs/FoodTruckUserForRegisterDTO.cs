namespace FoodTruckRodeo.API.DTOs
{
    public class FoodTruckUserForRegisterDTO
    {
        
    public int Id { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsActiveFoodTruck { get; set; }

    // EF Core Relationships: 
    // FoodTruck (many) and User (many) Models 
    public int FoodTruckId { get; set; }

    public int UserId { get; set; }
    }
}