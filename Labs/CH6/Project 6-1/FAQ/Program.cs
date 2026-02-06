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

app.MapControllers();

app.Run();
