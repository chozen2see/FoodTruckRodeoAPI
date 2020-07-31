namespace Models
{
  public class Item
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public string Size { get; set; }

    public bool IsSoldOut { get; set; }

    // EF Core Relationships: 
    // Menu(one) and Item (many) Models 
    public int MenuId { get; set; }
    public Menu Menu { get; set; }
  }
}