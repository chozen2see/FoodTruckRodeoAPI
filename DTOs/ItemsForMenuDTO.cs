namespace FoodTruckRodeo.API.DTOs
{
  public class ItemsForMenuDTO
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public string Size { get; set; }

    public bool IsSoldOut { get; set; }

    public int SortOrder { get; set; }

  }
}