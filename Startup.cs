using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data;
using FoodTruckRodeo.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FoodTruckRodeo.API
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
      services.AddDbContext<DataContext>(db => db.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

      // added NuGet package for Microsoft.AspNetCore.Mvc.NewtonsoftJson
      // AddNewtonsoftJson() will act as if using .NET Core 2.2 with Newtonsoft JSON
      services.AddControllers().AddNewtonsoftJson(
        // Added this option to handle the error:
        // fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
        // An unhandled exception has occurred while executing the request.
        // Newtonsoft.Json.JsonSerializationException: Self referencing loop detected for property 'menu' with type 'Models.Menu'. Path 'items[0]'.
        opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
      );

      // make CORS service available to be used as middleware
      services.AddCors();

      // instance of service is created once per scope
      // <Interface, Implementation>
      // Implementation can change at any point as long as signature of methods dont change in the Interface
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();

      // because we used [Authorize] attribute in Controller we need to add the service here with options
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
      {
        options.TokenValidationParameters =
        new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
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
      else
      {
        // Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline. The request will not be re-executed if the response has already started.
        app.UseExceptionHandler(builder =>
        {
          // Adds a terminal middleware delegate to the application's request pipeline.
          builder.Run(async context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // store the error so it can be accessed
            var error = context.Features.Get<IExceptionHandlerFeature>();

            // if it's an actual error
            if (error != null)
            {
              // use Extension to add messages to header
              context.Response.AddApplicationError(error.Error.Message);
              // write error message into HTTP Response
              await context
              // use an extension method to add custom errors to response
              .Response.WriteAsync(error.Error.Message);
            }
          });
        });
      }

      // app.UseHttpsRedirection();

      // use routing
      app.UseRouting();

      // use CORS
      app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

      // use authent
      app.UseAuthentication(); // and add middleware here
      app.UseAuthorization();  // to pipeline for [Authorize]

      // use endpoints
      app.UseEndpoints(endpoints =>
      {
        // map controller endpoints to app so api knows how to route requests
        endpoints.MapControllers();
      });
    }
  }
}
