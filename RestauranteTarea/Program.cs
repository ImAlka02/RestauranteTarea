using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestauranteTarea.Models.Entities;
using RestauranteTarea.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<NeatContext>(x => x.UseMySql("server=localhost;user=root;password=root;database=neat",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));

builder.Services.AddTransient<MenuRepository>();
builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<Repository<Clasificacion>>();

var app = builder.Build();

app.UseFileServer();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapDefaultControllerRoute();

app.Run();
