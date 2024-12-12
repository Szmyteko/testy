using System.Security.Claims;
using APT_Market.Data;
using APT_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace APT_Market.Controllers;

[Authorize]
public class AdminPanelController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly ApplicationDbContext _context;
    public AdminPanelController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }
    
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
                Roles = roles,
                PhoneNumber = user.PhoneNumber,
            });
        }

        return View(usersWithRoles);
    }

// Akcja GET: Create
    public IActionResult Create()
    {
        // Pobierz role z bazy danych
        var roles = _roleManager.Roles.Select(r => new SelectListItem
        {
            Value = r.Name,
            Text = r.Name
        }).ToList();

        ViewBag.Roles = roles;

        // Zwróć pusty model widoku do widoku
        return View(new UsersViewModel());
    }


    // Akcja POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsersViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            // Utworzenie użytkownika z hasłem
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Przypisanie użytkownika do wybranej roli
                if (!string.IsNullOrEmpty(model.SelectedRole))
                {
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);
                }

                return RedirectToAction("Index"); // Powrót do listy użytkowników
            }

            // Obsługa błędów
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Błąd walidacji: {error.ErrorMessage}");
                }
            }
        }

        // W przypadku błędu ponownie pobierz role
        ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name).ToList());
        return View(model);
    }
    
    // Akcja GET: Details
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID użytkownika jest wymagane.");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("Użytkownik nie został znaleziony.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var viewModel = new UsersViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = roles.ToList(),
            UserName = user.UserName
        };

        return View(viewModel);
    }
    // Akcja POST: Details
    
    // Akcja GET: Delete
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID użytkownika jest wymagane.");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("Użytkownik nie został znaleziony.");
        }

        var viewModel = new UsersViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
        };

        return View(viewModel);
    }
    // Akcja POST: Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID użytkownika jest wymagane.");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("Użytkownik nie został znaleziony.");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Index"); // Powrót do listy użytkowników jeśli udana akcja
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View("Delete", new UsersViewModel { UserId = id, UserName = user.UserName });
    }

}