using Bookstore_App.Models.DomainModels;
using Bookstore_App.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bookstore_App.Models.Account;
using System.Security.Claims;



namespace Bookstore_App.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

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
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await _userManager.AddToRoleAsync(user, "Admin");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        TempData["SuccessMessage"] = "Registration successful. Welcome to Tuxedo Books!";
        return RedirectToAction("Index", "Home");
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

        var user = await _userManager.FindByNameAsync(userName);
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
        await _signInManager.SignOutAsync();

        // Clear cart on logout
        Response.Cookies.Delete("bookstore_cart");

        return RedirectToAction("Index", "Home");
    }
}
