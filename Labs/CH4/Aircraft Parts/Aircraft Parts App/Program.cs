using Aircraft_Parts_App.Models;
using Aircraft_Parts_App.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PartContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalDbCS")));
    
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Parts}/{action=Index}/{id?}");

app.Run();
