using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

namespace Data
{
  public class Seed
  {
    // take json seed data objects and serialize them into user objects to match user model class

    public static void SeedFoodTrucks(DataContext context)
    {
      if (!context.FoodTrucks.Any())
      {
        var foodTruckData = System.IO.File.ReadAllText("Data/FoodTruckSeedData.json");

        var foodTrucks = JsonConvert.DeserializeObject<List<FoodTruck>>(foodTruckData);

        foreach (var foodTruck in foodTrucks) {
          context.FoodTrucks.Add(foodTruck);
        }

        context.SaveChanges();
      }

    }

    public static void SeedFoodTruckUserData(DataContext context)
    {
      if (!context.FoodTruckUsers.Any())
      {
        var foodTruckData = System.IO.File.ReadAllText("Data/FoodTruckUserSeedData.text");

        // seed remaining data
        context.Database.ExecuteSqlRaw(foodTruckData);
        context.SaveChanges();
      }
    }

        public static void SeedMenuData(DataContext context)
    {
      if (!context.Menus.Any())
      {
        var menuData = System.IO.File.ReadAllText("Data/MenuSeedData.text");

        // seed remaining data
        context.Database.ExecuteSqlRaw(menuData);
        context.SaveChanges();
      }
    }

        public static void SeedItemData(DataContext context)
    {
      if (!context.Items.Any())
      {
        var menuData = System.IO.File.ReadAllText("Data/ItemSeedData.text");

        // seed remaining data
        context.Database.ExecuteSqlRaw(menuData);
        context.SaveChanges();
      }
    }

    public static void SeedUsers(DataContext context)
    {
      // check to see if users already seeded
      if (!context.Users.Any())
      {
        var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

        var users = JsonConvert.DeserializeObject<List<User>>(userData);

        foreach (var user in users)
        {
          byte[] passwordHash, passwordSalt;
          CreatePasswordHash("password", out passwordHash, out passwordSalt);

          user.PasswordHash = passwordHash;
          user.PasswordSalt = passwordSalt;
          user.Username = user.Username.ToLower();

          context.Users.Add(user);
        }

        context.SaveChanges();
      }

    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      // disposes of everything inside once code is executed
      using (
          // Initializes a new instance of the System.Security.Cryptography.HMACSHA512 class with a randomly generated key.

          var hmac = new System.Security.Cryptography.HMACSHA512()
      )
      {
        passwordSalt = hmac.Key;

        // ComputeHash takes in byte array. Must Convert password to byte array using UTF8.GetBytes funct
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }


    }
  }
}