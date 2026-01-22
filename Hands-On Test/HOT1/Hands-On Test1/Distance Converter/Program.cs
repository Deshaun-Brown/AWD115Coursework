var builder = WebApplication.CreateBuilder(args);

// Register MVC controller services
builder.Services.AddControllersWithViews();

// Register session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Distance}/{action=Index}/{id?}");

app.Run();
