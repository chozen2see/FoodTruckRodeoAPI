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
    // Task<FoodTruckUser> GetFoodTruckUserByFoodTruckId(int id);
    // Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsersByFoodTruckId(int id);

    // Task<FoodTruckUser> GetFoodTruckUserByUserId(int id);
    // Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsersByUserId(int id);

    Task<IEnumerable<User>> GetUsers();

    // get a user
    Task<User> GetUser(int id);

    // get all menus
    Task<IEnumerable<Menu>> GetMenus(int foodTruckId);

    // get a menu
    Task<Menu> GetMenu(int foodTruckId, int id);

    // get all items - int foodTruckId, int menuId
    Task<IEnumerable<Item>> GetItems(int foodTruckId, int menuId);

    // get a item - 
    Task<Item> GetItem(int foodTruckId, int menuId, int id);

    // get a cart
    Task<Cart> GetCart(int foodtruckid, int userId, int id);

    // get order
    Task<Cart> GetOrder(int foodtruckid, int userId, int id, int filled);

    // get all carts - int userId
    Task<IEnumerable<Cart>> GetOrderHistory();

    // get all cart details - int userId, int cartId
    Task<IEnumerable<CartItemDetail>> GetCartItemDetails();

    // get a cart detail - int userId, int cartId, 
    Task<CartItemDetail> GetCartItemDetail(int id);

    // get all calendar events
    Task<IEnumerable<CalendarEvent>> GetCalendarEvents(int foodTruckId);

    // get a calendar event
    Task<CalendarEvent> GetCalendarEvent(int foodTruckId, int id);
  }
}