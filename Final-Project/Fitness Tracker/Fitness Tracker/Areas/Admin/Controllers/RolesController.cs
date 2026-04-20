using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/roles")]
[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        var model = new List<AdminRoleListItemViewModel>();

        foreach (var role in roles)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            model.Add(new AdminRoleListItemViewModel
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty,
                UserCount = usersInRole.Count
            });
        }

        return View("~/Areas/Admin/Views/Roles/Index.cshtml", model);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View("~/Areas/Admin/Views/Roles/Create.cshtml");
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name)
    {
        name = name?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError(string.Empty, "Role name is required.");
        }
        else if (await _roleManager.RoleExistsAsync(name))
        {
            ModelState.AddModelError(string.Empty, "That role already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View("~/Areas/Admin/Views/Roles/Create.cshtml");
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("~/Areas/Admin/Views/Roles/Create.cshtml");
        }

        TempData["Success"] = "Role created successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
        {
            return NotFound();
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name!);
        var model = new AdminRoleDeleteViewModel
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Users = users.Select(u => u.Email ?? u.UserName ?? u.Id).ToList()
        };

        return View("~/Areas/Admin/Views/Roles/Delete.cshtml", model);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role is null)
        {
            return NotFound();
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name!);
        foreach (var user in users)
        {
            await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }

        await _roleManager.DeleteAsync(role);
        TempData["Success"] = "Role deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
