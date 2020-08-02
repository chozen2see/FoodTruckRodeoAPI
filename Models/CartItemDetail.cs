namespace Models
{
  public class CartItemDetail
  {
    public int Id { get; set; }

    public int Quantity { get; set; }

    // EF Core Relationships: 
    // Cart (many) and Item (many) Models 
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; }
  }
}