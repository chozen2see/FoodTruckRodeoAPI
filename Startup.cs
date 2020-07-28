using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
      services.AddDbContext<DataContext>(db => db.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
      services.AddControllers();
      // make CORS service available to be used as middleware
      services.AddCors();

      // instance of service is created once per scope
      // <Interface, Implementation>
      // Implementation can change at any point as long as signature of methods dont change in the Interface
      services.AddScoped<IAuthRepository, AuthRepository>();

      // because we used [Authorize] attribute in Controller we need to add the service here with options
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
        options.TokenValidationParameters = 
        new TokenValidationParameters {
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
