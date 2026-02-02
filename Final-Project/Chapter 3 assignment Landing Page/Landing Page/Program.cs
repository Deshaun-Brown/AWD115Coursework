using Landing_Page.Models;


using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register MVC controller services
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Page}/{action=Index}/{id?}");

app.Run();
