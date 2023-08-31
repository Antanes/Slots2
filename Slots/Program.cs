using Microsoft.EntityFrameworkCore;
using Slots.Data;
using Slots.Data.Services;

namespace Slots
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\Anton\\source\\repos\\Slots\\Slots\\App_Data");

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connection));


            builder.Services.AddScoped<IDrinkService, DrinkService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Drink}/{action=GetDrinks}/{id?}");

            app.Run();
        }
    }
}