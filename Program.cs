using System;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodTruckRodeo.API
{
  public class Program
  {
    // starts here
    public static void Main(string[] args)
    {
      // add seed method inside Main method
      // access datacontext to pass as argument to seed users method

      // CreateHostBuilder(args).Build().Run;
      var host = CreateHostBuilder(args).Build();
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        // no error handling exception avail here
        try
        {
          var context = services.GetRequiredService<DataContext>();

          // Applies any pending migrations for the context to the database. Will create the database if it does not already exist.
          context.Database.Migrate();

          // seed data if none exist
          Seed.SeedFoodTrucks(context);
          Seed.SeedUsers(context);
          Seed.SeedFoodTruckUserData(context);
          Seed.SeedMenuData(context);
          Seed.SeedItemData(context);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "User Seed Error: An error occurred during migration!");
        }
      }
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args) // kestel server part of .net sdk. 
        // reads config from appsettings.Developnent.json (dev) or appsettings.json (prod) 
            .ConfigureWebHostDefaults(webBuilder =>
            {
              // use Startup.cs - what parts of framework are being used
              webBuilder.UseStartup<Startup>();
            });
  }
}
