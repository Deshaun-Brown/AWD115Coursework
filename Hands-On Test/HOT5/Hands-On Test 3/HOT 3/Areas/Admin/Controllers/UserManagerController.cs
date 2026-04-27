using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmaceuticals.Models;
using Pharmaceuticals.ViewModels;

namespace Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin/usermanager")]
public class UserManagerController : Controller
{
    private const string AdminRoleName = "Admin";
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManagerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
            .OrderBy(u => u.Email)
            .ToListAsync();

        var userItems = new List<UserManagerUserItemViewModel>();

        foreach (var user in users)
        {
            userItems.Add(new UserManagerUserItemViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = await _userManager.IsInRoleAsync(user, AdminRoleName)
            });
        }

        var model = new UserManagerViewModel
        {
            AdminRoleExists = await _roleManager.RoleExistsAsync(AdminRoleName),
            Users = userItems
        };

        return View(model);
    }

    [HttpPost("create-admin-role")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAdminRole()
    {
        if (!await _roleManager.RoleExistsAsync(AdminRoleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(AdminRoleName));
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? "Admin role created successfully."
                : string.Join(" ", result.Errors.Select(e => e.Description));
        }
        else
        {
            TempData["Success"] = "Admin role already exists.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("add-admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToAdminRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            TempData["Error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }

        if (!await _roleManager.RoleExistsAsync(AdminRoleName))
        {
            TempData["Error"] = "Admin role does not exist. Create it first.";
            return RedirectToAction(nameof(Index));
        }

        if (!await _userManager.IsInRoleAsync(user, AdminRoleName))
        {
            var result = await _userManager.AddToRoleAsync(user, AdminRoleName);
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? "User added to Admin role."
                : string.Join(" ", result.Errors.Select(e => e.Description));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("remove-admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromAdminRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            TempData["Error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }

        var currentUserId = _userManager.GetUserId(User);
        if (user.Id == currentUserId)
        {
            TempData["Error"] = "You cannot remove your own Admin role.";
            return RedirectToAction(nameof(Index));
        }

        if (await _userManager.IsInRoleAsync(user, AdminRoleName))
        {
            var result = await _userManager.RemoveFromRoleAsync(user, AdminRoleName);
            TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
                ? "User removed from Admin role."
                : string.Join(" ", result.Errors.Select(e => e.Description));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete-user")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            TempData["Error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }

        var currentUserId = _userManager.GetUserId(User);
        if (user.Id == currentUserId)
        {
            TempData["Error"] = "You cannot delete your own account from here.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);
        TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded
            ? "User deleted successfully."
            : string.Join(" ", result.Errors.Select(e => e.Description));

        return RedirectToAction(nameof(Index));
    }
}
