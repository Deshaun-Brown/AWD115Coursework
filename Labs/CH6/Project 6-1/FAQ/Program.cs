using FAQ.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FaqContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FaqContext")));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

SeedData.Initialize(app);

// Custom routes (static segments distinguish topic vs category)
app.MapControllerRoute(
    name: "topic",
    pattern: "topic/{topicId}/category/{categoryId}/",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "topicOnly",
    pattern: "topic/{topicId}/",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "categoryOnly",
    pattern: "category/{categoryId}/",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/");

app.Run();
