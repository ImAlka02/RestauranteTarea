using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestauranteTarea.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<NeatContext>(x => x.UseMySql("server=localhost;user=root;password=root;database=neat",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));
var app = builder.Build();

app.UseFileServer();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapDefaultControllerRoute();

app.Run();
