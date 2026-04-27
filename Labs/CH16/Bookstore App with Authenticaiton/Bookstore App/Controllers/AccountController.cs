using Bookstore_App.Models.DomainModels;
using Bookstore_App.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bookstore_App.Models.Account;
using System.Security.Claims;
using System.Text.Json;



namespace Bookstore_App.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private const string CartCookieBaseName = "bookstore_cart";

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userNameValue = model.UserName?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(userNameValue))
        {
            ModelState.AddModelError(nameof(model.UserName), "UserName is required.");
            return View(model);
        }

        var user = new User
        {
            UserName = userNameValue,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DisplayName = userNameValue
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        if (model.IsAdmin)
        {
            if (!await _role_manager_check())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await _userManager.AddToRoleAsync(user, "Admin");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        // Migrate anonymous cart into per-user cart
        await MigrateAnonymousCartToUserAsync(user.Id);

        TempData["SuccessMessage"] = "Registration successful. Welcome to Tuxedo Books!";
        return RedirectToAction("Index", "Home");
    }

    private async Task<bool> _role_manager_check()
    {
        return await _roleManager.RoleExistsAsync("Admin");
    }

    private async Task MigrateAnonymousCartToUserAsync(string userId)
    {
        try
        {
            var anonJson = Request.Cookies[CartCookieBaseName];
            if (string.IsNullOrWhiteSpace(anonJson)) return;

            var anonCart = JsonSerializer.Deserialize<Dictionary<int, int>>(anonJson) ?? new Dictionary<int, int>();
            if (anonCart.Count == 0) 
            {
                Response.Cookies.Delete(CartCookieBaseName);
                return;
            }

            var userCookieName = CartCookieBaseName + "_" + userId;
            var existingJson = Request.Cookies[userCookieName];
            var existingCart = string.IsNullOrWhiteSpace(existingJson)
                ? new Dictionary<int, int>()
                : JsonSerializer.Deserialize<Dictionary<int, int>>(existingJson) ?? new Dictionary<int,int>();

            // Merge
            foreach (var kvp in anonCart)
            {
                if (existingCart.ContainsKey(kvp.Key)) existingCart[kvp.Key] += kvp.Value;
                else existingCart[kvp.Key] = kvp.Value;
            }

            var mergedJson = JsonSerializer.Serialize(existingCart);
            Response.Cookies.Append(userCookieName, mergedJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(14),
                IsEssential = true,
                HttpOnly = false,
                SameSite = SameSiteMode.Lax,
                Secure = Request.IsHttps
            });

            // Remove anonymous cookie
            Response.Cookies.Delete(CartCookieBaseName);
        }
        catch
        {
            // ignore migration errors
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl ?? string.Empty
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userName = model.UserName?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(userName))
        {
            ModelState.AddModelError(nameof(model.UserName), "Please enter a username.");
            return View(model);
        }

        var user = await _userManager.FindByNameAsync(userName) ?? await _userManager.FindByEmailAsync(userName);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid UserName or password.");
            return View(model);
        }

        var passwordResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (!passwordResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid UserName or password.");
            return View(model);
        }

        var claims = new List<Claim>();
        if (!string.IsNullOrWhiteSpace(user.LastName))
        {
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
        }

        await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);

        // Migrate anonymous cart into per-user cart
        await MigrateAnonymousCartToUserAsync(user.Id);

        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // capture user id before sign out
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _signInManager.SignOutAsync();

        // Clear anonymous cart and user-specific cart
        Response.Cookies.Delete(CartCookieBaseName);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            Response.Cookies.Delete(CartCookieBaseName + "_" + userId);
        }

        return RedirectToAction("Index", "Home");
    }

    // ... rest of controller unchanged ...
}
