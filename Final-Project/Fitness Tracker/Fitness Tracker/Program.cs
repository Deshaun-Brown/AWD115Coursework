using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add session support (ISessionStore) in case middleware is used elsewhere
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // sensible defaults; adjust as needed
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();




builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesOrdersDB")));

var apiKey = Environment.GetEnvironmentVariable("FITNESS_TRACKER_API_KEY");
builder.Services.AddChatClient(sp => new OpenAIClient(apiKey).GetChatClient("gpt-4o").AsIChatClient());

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Require unique email addresses for user accounts to prevent duplicate registrations
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

// Register DatabaseAgent for reading from the database
builder.Services.AddScoped<IDatabaseAgent, DatabaseAgent>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedData.InitializeAsync(scope.ServiceProvider);
}


app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.MapRazorPages();


app.Run();
