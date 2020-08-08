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


    // get all calendar events
    Task<IEnumerable<CalendarEvent>> GetCalendarEvents(int foodTruckId);

    // get a calendar event
    Task<CalendarEvent> GetCalendarEvent(int foodTruckId, int id);


    // get all menus
    Task<IEnumerable<Menu>> GetMenus(int foodTruckId);

    // get a menu
    Task<Menu> GetMenu(int foodTruckId, int id);



    Task<Cart> CreateCart(Cart cart, int foodTruckId, int userId);

    // get a cart
    Task<Cart> GetCart(int foodtruckid, int userId, int id);

    // get all cart details - int userId, int cartId
    Task<IEnumerable<CartItemDetail>> GetCartItemDetails();

    // get a cart detail - int userId, int cartId, 
    Task<CartItemDetail> GetCartItemDetail(int id);

    Task<Cart> DeleteCart(int id, int foodTruckId, int userId);

    // complete purchase of items in cart
    Task<Cart> CompletePurchase(int id);

    // get order
    Task<Cart> GetOrder(int foodtruckid, int userId, int id);

    // fill order
    Task<Cart> FillOrder(int id);

    // get all filled orders 
    Task<IEnumerable<Cart>> GetOrderHistory(int foodtruckid, int userId);


// update qty of item in cart
    Task<CartItemDetail> AddCartItem(int id, int foodTruckId, int userId, int itemId, int qty);

    // get a item - 
    Task<Item> GetItem(int foodTruckId, int menuId, int id);

    // get all items - int foodTruckId, int menuId
    Task<IEnumerable<Item>> GetItems(int foodTruckId, int menuId);


    // update qty of item in cart
    Task<CartItemDetail> UpdateItem(int id, int itemId, int qty);

    // delete item from cart
    Task<Cart> DeleteItem(int id, int foodTruckId, int userId,int itemId);
  }
}