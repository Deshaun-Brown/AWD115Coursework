using System.Linq;
using Bookstore_App.Areas.Admin.Models;
using Bookstore_App.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRows = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRows.Add(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                RolesText = roles.Count == 0 ? "User" : string.Join(", ", roles)
            });
        }

        var vm = new ManageUsersViewModel
        {
            Users = userRows.OrderBy(u => u.UserName).ToList(),
            Roles = _roleManager.Roles.Select(r => r.Name ?? string.Empty).OrderBy(r => r).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await _userManager.IsInRoleAsync(user, "Admin"))
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            await _userManager.RemoveFromRoleAsync(user, "Admin");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRole(string roleName)
    {
        if (string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction(nameof(Index));
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role is not null)
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction(nameof(Index));
    }
}
