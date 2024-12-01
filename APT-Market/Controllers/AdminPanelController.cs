using APT_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APT_Market.Controllers;

[Authorize]
public class AdminPanelController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminPanelController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // public async Task<IActionResult> Index()
    // {
    //     var user = await _userManager.GetUserAsync(User);
    //     return View();
    // }
    public async Task<IActionResult> AddRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null && await _roleManager.RoleExistsAsync(roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
            return Ok("Rola została dodana.");
        }
        return BadRequest("Nie udało się przypisać roli.");
    }
    
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var usersWithRoles = new List<UsersViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UsersViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }

        return View(usersWithRoles);
    }
}