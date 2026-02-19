  using Aircraft_Parts_App.Models;
using Aircraft_Parts_App.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Dependency injection for DbContext - Register PartContext
builder.Services.AddDbContext<PartContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalDbCS")));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});
    
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Parts}/{action=Index}/{id?}/{slug?}");

app.Run();
