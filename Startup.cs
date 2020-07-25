using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      // allows access to appsettings files
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // dependency injection container. when something needs to be consumed by another part of application it should be added as a service here for injection somewhere else in app

      // add DataContext class as a service with (options)
      // connecting to db , so need to specify
      // database provider: sqlite
      // connection string: 
      services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
      services.AddControllers();
      // make CORS service available to be used as middleware
      services.AddCors();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // used to configure HTTP Request pipeline. as request comes in to API, request goes through this pipeline. this is middleware to interact with request through pipeline - ORDER MATTERS IN PIPELINE
      if (env.IsDevelopment())
      {
        // if dev environment and there is an exception use dev friendly page to display
        // global exception handler
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();

      // use routing
      app.UseRouting();

      // use CORS
      app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      // use authent
      app.UseAuthorization();

      // use endpoints
      app.UseEndpoints(endpoints =>
      {
        // map controller endpoints to app so api knows how to route requests
        endpoints.MapControllers();
      });
    }
  }
}
