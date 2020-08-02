using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Data
{
  public interface IFoodTruckRepository
  {
    // add type of T (user, foodtruckuser, cart, cart detail, contact request, etc)
    void Add<T>(T entity) where T : class;
    // delete type of T 
    void Delete<T>(T entity) where T : class;

    // 0 changes (false) - no changes or problem saving changes 
    //or > 0 changes (true)
    Task<bool> SaveAll();

    // get all food trucks
    Task<IEnumerable<FoodTruck>> GetFoodTrucks();

    // get a food truck
    Task<FoodTruck> GetFoodTruck(int id);

    Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsers();

    Task<IEnumerable<User>> GetUsers();
    
    // get a user
    Task<User> GetUser(int id);

    // get all menus
    Task<IEnumerable<Menu>> GetMenus();

    // get a menu
    Task<Menu> GetMenu(int id);

    // get all items
    Task<IEnumerable<Item>> GetItems();

    // get a item
    Task<Item> GetItem(int id);

    // get all carts
    Task<IEnumerable<Cart>> GetCarts();

    // get a cart
    Task<Cart> GetCart(int id);

    // get all cart details
    Task<IEnumerable<CartItemDetail>> GetCartItemDetails();

    // get a cart detail
    Task<CartItemDetail> GetCartItemDetail(int id);

    // get all calendar events
    Task<IEnumerable<CalendarEvent>> GetCalendarEvents();

    // get a calendar event
    Task<CalendarEvent> GetCalendarEvent(int id);
  }
}