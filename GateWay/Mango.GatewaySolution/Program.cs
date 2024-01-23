using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

namespace Mango.GatewaySolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {

                  options.Authority = "https://localhost:44365/";
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateAudience = false
                  };

              });

            builder.Services.AddOcelot();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.UseOcelot();
            app.Run();
        }
    }
}