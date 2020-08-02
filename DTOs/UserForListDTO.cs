namespace FoodTruckRodeo.API.DTOs
{
  public class UserForListDTO
  {
    public int Id { get; set; }

    public string Username { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public int FoodTruckId { get; set; }
  }
}