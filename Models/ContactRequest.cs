namespace Models
{
  public class ContactRequest
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Message { get; set; }

    // EF Core Relationships: 
    // FoodTruck (one) and Contact (many) Models  
    public int FoodTruckId { get; set; }
    public FoodTruck FoodTruck { get; set; }
  }
}