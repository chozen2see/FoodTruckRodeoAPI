using System;

namespace Models
{
  public class CalendarEvent
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Location { get; set; }

    public string Address { get; set; }

    public string Address2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    // EF Core Relationships: 
    // FoodTruck (one) and CalendarEvent (many) Models 
    public int FoodTruckId { get; set; }
    public FoodTruck FoodTruck { get; set; }
  }
}