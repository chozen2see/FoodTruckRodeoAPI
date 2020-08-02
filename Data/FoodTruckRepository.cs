using System.Collections.Generic;
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

    public async Task<CalendarEvent> GetCalendarEvent(int id)
    {
      var calendarEvent = await _context.CalendarEvents.FirstOrDefaultAsync(e => e.Id == id);
      return calendarEvent;
    }

    public async Task<IEnumerable<CalendarEvent>> GetCalendarEvents()
    {
      var calendarEvents = await _context.CalendarEvents.ToListAsync();
      return calendarEvents;
    }

    public async Task<Cart> GetCart(int id)
    {
      var cart = await _context.Carts.Include(cid => cid.CartItemDetails).FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<IEnumerable<Cart>> GetCarts()
    {
      var carts = await _context.Carts.Include(cid => cid.CartItemDetails).ToListAsync();
      return carts;
    }

    public async Task<FoodTruck> GetFoodTruck(int id)
    {
      var foodTruck = await _context.FoodTrucks.Include(m => m.Menus).FirstOrDefaultAsync(ft => ft.Id == id);
      return foodTruck;
    }

    public async Task<IEnumerable<FoodTruck>> GetFoodTrucks()
    {
      var foodTrucks = await _context.FoodTrucks.ToListAsync();
      return foodTrucks;
    }
    public async Task<FoodTruckUser> GetFoodTruckUser(int id)
    {
      var foodTruckUser = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).FirstOrDefaultAsync(ftu => ftu.Id == id);
      return foodTruckUser;
    }

    public async Task<IEnumerable<FoodTruckUser>> GetFoodTruckUsers()
    {
      var foodTruckUsers = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).ToListAsync();
      return foodTruckUsers;
    }

    public async Task<Item> GetItem(int id)
    {
      var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
      return item;
    }

    public async Task<IEnumerable<Item>> GetItems()
    {
      var items = await _context.Items.ToListAsync();
      return items;
    }

    public async Task<Menu> GetMenu(int id)
    {
      var menu = await _context.Menus.Include(i => i.Items).FirstOrDefaultAsync(m => m.Id == id);
      return menu;
    }

    public async Task<IEnumerable<Menu>> GetMenus()
    {
      var menus = await _context.Menus.ToListAsync();
      return menus;
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
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