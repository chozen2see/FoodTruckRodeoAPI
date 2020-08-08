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
      // TODO: WILL THIS MESS UP ANOTHER SECTION IF I ADD INCLUDES BACK
        .Include("CartItemDetails.Item")
        .Include(c => c.CartItemDetails)
        .Include(c => c.FoodTruckUser)
        .Where(c =>
        c.FoodTruckUser.FoodTruckId == foodtruckid &&
        c.FoodTruckUser.UserId == userId &&
        c.IsPurchaseComplete == false &&
        c.IsOrderFilled == false
        )
        .FirstOrDefaultAsync(c => c.Id == id || id == 0);

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
    public async Task<Cart> GetOrder(int foodTruckId, int userId, int id)
    {
      var cart = await _context.Carts
        .Include("CartItemDetails.Item")
        // .Include(c => c.CartItemDetails)
        .Include(c => c.FoodTruckUser)
        .Where(c =>
        c.FoodTruckUser.FoodTruckId == foodTruckId &&
        c.FoodTruckUser.UserId == userId &&
        c.IsPurchaseComplete == true
        )
        .FirstOrDefaultAsync(c => c.Id == id);

      return cart;
    }

    // User historical orders.
    // IsPurchased == 1  | IsOrderFilled == 1
    public async Task<IEnumerable<Cart>> GetOrderHistory(int foodtruckid, int userId)
    {
      var carts = await _context.Carts
        .Include("CartItemDetails.Item")
        // .Include(c => c.CartItemDetails)
        .Include(c => c.FoodTruckUser)
        .Where(c =>
        c.FoodTruckUser.FoodTruckId == foodtruckid &&
        c.FoodTruckUser.UserId == userId &&
        c.IsPurchaseComplete == true &&
        c.IsOrderFilled == true
        ).ToListAsync();

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


    // public async Task<FoodTruckUser> GetFoodTruckUserByUserId(int userId)
    // {
    //   var foodTruckUser = await _context.FoodTruckUsers.Include(ft => ft.FoodTruck).FirstOrDefaultAsync(ftu => ftu.Id == userId);
    //   return foodTruckUser;
    // }

    public async Task<int> GetFoodTruckUserId(int foodTruckId, int userId)
    {
      var foodTruckUser = await _context.FoodTruckUsers
      .Where(ftu =>
        ftu.FoodTruckId == foodTruckId &&
        ftu.UserId == userId)
      .FirstOrDefaultAsync();

      return foodTruckUser.Id;
    }

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

    public async Task<Cart> CreateCart(Cart cart, int foodTruckId, int userId)
    {
      var newCart = default(Cart);
      newCart = new Cart()
      {
        SubTotal = 0,
        Tax = 0,
        Total = 0,
        IsPurchaseComplete = false,
        IsOrderFilled = false,

        // GET FOODTRUCKUSERID BASED ON FOODTRUCKID & USERID
        FoodTruckUserId = await GetFoodTruckUserId(foodTruckId, userId)
      };

      cart = newCart;

      // CREATE CART
      await _context.Carts.AddAsync(cart);
      await _context.SaveChangesAsync();
      return cart;
    }

    public async Task<Cart> UpdateCart(Cart cart)
    {

      var subtotal = 0.0;

      var cartItemDetails = await _context.CartItemDetails
        .Include(cid => cid.Item)
        .Where(cid => cid.CartId == cart.Id)
        .ToListAsync();

      foreach (var item in cartItemDetails)
      {
        subtotal = subtotal + (item.Quantity * item.Item.Price);
      }

      cart.SubTotal = Convert.ToSingle(subtotal);
      // cart.Tax = (float)(cart.SubTotal * GetTaxRate());
      cart.Tax = Convert.ToSingle(Math.Round(cart.SubTotal * GetTaxRate(), 2));
      cart.Total = cart.SubTotal + cart.Tax;

      await SaveAll();

      return cart;
    }

    private float GetTaxRate()
    {
      return 0.08F;
    }

    public async Task<CartItemDetail> AddCartItem(int id, int foodTruckId, int userId, int itemId, int qty)
    {
      // 1. CHECK IF CART EXISTS 
      // // IsPurchased == 0  | IsOrderFilled == 0
      var cart = await GetCart(foodTruckId, userId, id);

      if (cart == null)
      {
        // 2. IF NOT CREATE CART
        cart = await CreateCart(cart, foodTruckId, userId);
      }

      // 3. IS ITEM ALREADY IN CART? IF SO, UPDATE TO QTY 22.19
      if (_context.CartItemDetails.Any(
        c => c.CartId == cart.Id && c.ItemId == itemId
      ))
      {
        await UpdateItem(cart.Id, itemId, qty);
      }
      else
      {
        // 4. CREATE ITEM
        var cartItem = default(CartItemDetail);

        cartItem = new CartItemDetail()
        {
          Quantity = qty,
          ItemId = itemId,
          CartId = cart.Id
        };

        await _context.CartItemDetails.AddAsync(cartItem);
        await SaveAll();
      }

      // 5. UPDATE CART TOTALS 
      var updatedCart = await UpdateCart(cart);

      var itemToReturn = await _context.CartItemDetails.FirstOrDefaultAsync(
        c => c.CartId == cart.Id && c.ItemId == itemId
      );

      return itemToReturn;
    }

    public async Task<CartItemDetail> UpdateItem(int id, int itemId, int qty)
    {
      var item = await _context.CartItemDetails
        .Where(c =>
        c.CartId == id &&
        c.ItemId == itemId
        )
        .FirstOrDefaultAsync();

      if (item == null)
      {
        return null;
      }

      item.Quantity = qty;

      await SaveAll();

      return item;
    }

    public async Task<Cart> DeleteItem(int id, int foodTruckId, int userId, int itemId)
    {
      var item = await _context.CartItemDetails
        .Where(c =>
        c.CartId == id &&
        c.ItemId == itemId
        )
        .FirstOrDefaultAsync();

      if (item == null)
      {
        return null;
      }

      _context.CartItemDetails.Remove(item);

      await SaveAll();

      // get and update the cart
      var cart = await UpdateCart(await GetCart(foodTruckId, userId, id));

      return cart;
    }

    public async Task<Cart> DeleteCart(int id, int foodTruckId, int userId)
    {
      // GET FOODTRUCKUSERID BASED ON FOODTRUCKID & USERID
      var foodTruckUserId = await GetFoodTruckUserId(foodTruckId, userId);

      var cart = await _context.Carts
        .Where(c => c.Id == id && c.FoodTruckUserId == foodTruckUserId)
        .FirstOrDefaultAsync();

      if (cart == null)
      {
        return null;
      }

      var items = await _context.CartItemDetails
        .Where(c => c.CartId == cart.Id)
        .ToListAsync();

      foreach (var item in items)
      {
          _context.CartItemDetails.Remove(item);
      }

      _context.Carts.Remove(cart);

      await SaveAll();

      return cart;
    }

    public async Task<Cart> CompletePurchase(int id)
    {
      var order = await _context.Carts.FirstOrDefaultAsync(result => result.Id == id);

      if (order == null)
      {
        return null;
      }

      order.IsPurchaseComplete = true;

      await SaveAll();

      return order;
    }

    public async Task<Cart> FillOrder(int id)
    {
      var order = await _context.Carts.FirstOrDefaultAsync(result => result.Id == id);

      if (order == null)
      {
        return null;
      }

      order.IsOrderFilled = true;

      await SaveAll();

      return order;
    }

    public async Task<bool> SaveAll()
    {
      // if no changes saved return false otherwise return true
      return await _context.SaveChangesAsync() > 0;
    }
  }
}