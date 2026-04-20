using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/users")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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

        var model = new List<AdminUserListItemViewModel>();
        foreach (var user in users)
        {
            model.Add(new AdminUserListItemViewModel
            {
                Id = user.Id,
                Email = user.Email ?? user.UserName ?? string.Empty,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            });
        }

        return View("~/Areas/Admin/Views/Users/Index.cshtml", model);
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        var allRoles = await _roleManager.Roles
            .OrderBy(r => r.Name)
            .Select(r => r.Name!)
            .ToListAsync();

        var selectedRoles = (await _userManager.GetRolesAsync(user)).ToList();

        var model = new AdminUserEditViewModel
        {
            Id = user.Id,
            Email = user.Email ?? user.UserName ?? string.Empty,
            AllRoles = allRoles,
            SelectedRoles = selectedRoles
        };

        return View("~/Areas/Admin/Views/Users/Edit.cshtml", model);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, AdminUserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            model.AllRoles = await _roleManager.Roles.OrderBy(r => r.Name).Select(r => r.Name!).ToListAsync();
            return View("~/Areas/Admin/Views/Users/Edit.cshtml", model);
        }

        user.Email = model.Email;
        user.UserName = model.Email;
        await _userManager.UpdateAsync(user);

        var currentRoles = await _userManager.GetRolesAsync(user);
        var selectedRoles = model.SelectedRoles ?? new List<string>();

        var rolesToAdd = selectedRoles.Except(currentRoles).ToArray();
        var rolesToRemove = currentRoles.Except(selectedRoles).ToArray();

        if (rolesToAdd.Length > 0)
        {
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        if (rolesToRemove.Length > 0)
        {
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        }

        TempData["Success"] = "User updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        var model = new AdminUserListItemViewModel
        {
            Id = user.Id,
            Email = user.Email ?? user.UserName ?? string.Empty,
            Roles = (await _userManager.GetRolesAsync(user)).ToList()
        };

        return View("~/Areas/Admin/Views/Users/Delete.cshtml", model);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        await _userManager.DeleteAsync(user);
        TempData["Success"] = "User deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
