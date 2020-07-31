using Microsoft.AspNetCore.Http;

namespace FoodTruckRodeo.API.Helpers
{
  public static class Extensions
  {
    // general purpose extensions class that will be used for all global extensions

    // static so that an instance is not needed to access methods
    public static void AddApplicationError(
        this HttpResponse response, string message
    )
    {
      // use this method to add customer headers to response
      response.Headers.Add("Application-Error", message);
      // add to handle CORS
      response.Headers.Add(
          "Access-Control-Expose-Headers", "Application-Error");
      // allow any origin
      response.Headers.Add(
          "Access-Control-Allow-Origin", "*");
    }

  }
}