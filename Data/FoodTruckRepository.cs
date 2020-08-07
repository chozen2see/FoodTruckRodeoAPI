using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
  public class FoodTruckRepository : IFoodTruckRepository
  {
    private readonly DataContext _context;

    public FoodTruckRepository(DataContext context)
    {
      _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<CalendarEvent> GetCalendarEvent(int foodTruckId, int id)
    {
      var calendarEvent = await _context
        .CalendarEvents
        .Where(ce => ce.FoodTruckId == foodTruckId)
        .FirstOrDefaultAsync(e => e.Id == id);

      return calendarEvent;
    }

    public async Task<IEnumerable<CalendarEvent>> GetCalendarEvents(int foodTruckId)
    {
      var calendarEvents = await _context
        .CalendarEvents
        .Where(ce => ce.FoodTruckId == foodTruckId)
        .ToListAsync();
      return calendarEvents;
    }

    // User adding to cart. Not purchased.
    // IsPurchased == 0  | IsOrderFilled == 0
    public async Task<Cart> GetCart(int foodtruckid, int userId, int id)
    {

      var cart = await _context.Carts
        .Include("CartItemDetails.Item")
        // .Include(c => c.CartItemDetails)
        .Include(c => c.FoodTruckUser)
        .Where(c =>
        c.FoodTruckUser.FoodTruckId == foodtruckid &&
        c.FoodTruckUser.UserId == userId &&
        c.IsPurchaseComplete == false &&
        c.IsOrderFilled == false
        )
        .FirstOrDefaultAsync(c => c.Id == id);

      return cart;

    }

    public async Task<CartItemDetail> GetCartItemDetail(int id)
    {
      var cartItemDetail = await _context.CartItemDetails.Include(i => i.Item).FirstOrDefaultAsync(cid => cid.Id == id);
      return cartItemDetail;
    }

    public async Task<IEnumerable<CartItemDetail>> GetCartItemDetails()
    {
      var cartItemDetails = await _context.CartItemDetails.Include(i => i.Item).ToListAsync();
      return cartItemDetails;
    }

    // User purchased items in cart. Order has not been filled.
    // IsPurchased == 1  | IsOrderFilled == 0
    public async Task<Cart> GetOrder(int foodtruckid, int userId, int id, int filled)
    {
      var cart = await _context.Carts
        .Include(c => c.CartItemDetails)
        .Include(c => c.FoodTruckUser)
        .Where(c => c.FoodTruckUser.FoodTruckId == foodtruckid && c.FoodTruckUser.UserId == userId)
        // .Where(c => c.FoodTruckUser.UserId == userId)
        .FirstOrDefaultAsync(c => c.Id == id);
      return cart;
    }

    // User historical orders.
    // IsPurchased == 1  | IsOrderFilled == 1
    public async Task<IEnumerable<Cart>> GetOrderHistory()
    {
      var carts = await _context.Carts.Include(cid => cid.CartItemDetails).ToListAsync();
      return carts;
    }

    public async Task<FoodTruck> GetFoodTruck(int id)
    {
      var foodTruck = await _context.FoodTrucks
        .Include(ft => ft.Menus)
        .FirstOrDefaultAsync(ft => ft.Id == id);
      return foodTruck;
    }

    public async Task<IEnumerable<FoodTruck>> GetFoodTrucks()
    {
      var foodTrucks = await _context.FoodTrucks
        .Include(ft => ft.Menus)
        .ToListAsync();

      return foodTrucks;
    }


    public async Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsers()
    {
      var foodTruckUsers = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).ToListAsync();
      return foodTruckUsers;
    }

    //     public async Task<FoodTruckUser> GetFoodTruckUserByFoodTruckId(int id)
    // {
    //   var foodTruckUser = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).FirstOrDefaultAsync(ftu => ftu.FoodTruckId == id);
    //   return foodTruckUser;
    // }

    // public async Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsersByFoodTruckId(int id)
    // {
    //   var foodTruckUsers = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).ToListAsync();
    //   return foodTruckUsers;
    // }

    // public async Task<FoodTruckUser> GetFoodTruckUserByUserId(int id)
    // {
    //   var foodTruckUser = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).FirstOrDefaultAsync(ftu => ftu.Id == id);
    //   return foodTruckUser;
    // }

    // public async Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsersByUserId(int id)
    // {
    //   var foodTruckUsers = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).ToListAsync();
    //   return foodTruckUsers;
    // }

    public async Task<Item> GetItem(int foodTruckId, int menuId, int id)
    {

      var menu = await _context.Menus
      .Include(m => m.Items)
      .Where(m => m.FoodTruckId == foodTruckId && m.Id == menuId)
      .FirstOrDefaultAsync();

      var itemToReturn = default(Item);

      if (menu != null)
      {
        foreach (var item in menu.Items)
        {
          if (item.Id == id)
          {
            itemToReturn = item;
          }
        }
      }

      return itemToReturn;
    }

    public async Task<IEnumerable<Item>> GetItems(int foodTruckId, int menuId)
    {
      // var items = await _context.Items.ToListAsync();

      var menu = await _context.Menus
      .Include(m => m.Items)
      .Where(m => m.FoodTruckId == foodTruckId && m.Id == menuId)
      .FirstOrDefaultAsync();

      return menu != null ? menu.Items : Enumerable.Empty<Item>();
    }

    public async Task<Menu> GetMenu(int foodTruckId, int id)
    {
      var menu = await _context.Menus
        .Include(m => m.Items)
        .Where(m => m.FoodTruckId == foodTruckId)
        .FirstOrDefaultAsync(m => m.Id == id);
      return menu;
    }

    public async Task<IEnumerable<Menu>> GetMenus(int foodTruckId)
    {
      var menus = await _context.Menus
        .Include(m => m.Items)
        .Where(m => m.FoodTruckId == foodTruckId)
        .ToListAsync();
      return menus;
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
      var users = await _context.Users.
      Include(u => u.FoodTruckUsers).ToListAsync();
      return users;
    }

    public async Task<User> GetUser(int id)
    {
      var user = await _context.Users.Include(u => u.FoodTruckUsers).FirstOrDefaultAsync(u => u.Id == id);
      return user;
    }

    public async Task<bool> SaveAll()
    {
      // if no changes saved return false otherwise return true
      return await _context.SaveChangesAsync() > 0;
    }
  }
}