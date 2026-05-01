using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using Microsoft.Extensions.AI;
using Fitness_tracker.Services;
using FluentValidation;
using Fitness_Tracker.Validators;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// Add session support (ISessionStore) in case middleware is used elsewhere
builder.Services.AddDistributedMemoryCache();
// Configure session options as needed; these are just sensible defaults

builder.Services.AddSession(options =>
{
    // sensible defaults; adjust as needed
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});






builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesOrdersDB")));

var apiKey = Environment.GetEnvironmentVariable("FITNESS_TRACKER_API_KEY");
if (!string.IsNullOrWhiteSpace(apiKey))
{
    builder.Services.AddChatClient(_ => new OpenAIClient(apiKey).GetChatClient("gpt-4o").AsIChatClient());
}



// Register Identity with role support so RoleManager and role-based checks work.
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// Register our Development EmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Register Google external authentication (OpenID Connect/OAuth2)
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        // Request OpenID Connect scopes
        googleOptions.Scope.Add("openid");
        googleOptions.Scope.Add("profile");
        googleOptions.Scope.Add("email");
        googleOptions.SaveTokens = true;
    });

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.MapRazorPages();


app.Run();
