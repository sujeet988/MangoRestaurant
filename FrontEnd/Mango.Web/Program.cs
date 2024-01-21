using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<IProductService, ProductServices>();
            builder.Services.AddHttpClient<ICartService, CartService>();
            builder.Services.AddHttpClient<ICouponService, CouponService>();

            SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
            SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICouponService, CouponService>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
               .AddOpenIdConnect("oidc", options =>
               {
                   options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];
                   options.GetClaimsFromUserInfoEndpoint = true;
                   options.ClientId = "mango";
                   options.ClientSecret = "secret";
                   options.ResponseType = "code";
                   options.ClaimActions.MapJsonKey("role", "role", "role");
                   options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                   options.TokenValidationParameters.NameClaimType = "name";
                   options.TokenValidationParameters.RoleClaimType = "role";
                   options.Scope.Add("mango");
                   options.SaveTokens = true;

               });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}