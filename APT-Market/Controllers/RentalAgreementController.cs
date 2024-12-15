using APT_Market.Data;
using APT_Market.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APT_Market.Controllers;

public class RentalAgreementController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public RentalAgreementController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Rent(int id)
    {
        var property = await _context.Property.FindAsync(id);
        if (property == null || !property.IsAvailable)
        {
            return BadRequest("Lokal jest niedostępny.");
        }

        var model = new RentalAgreement
        {
            PropertyId = property.Id,
            Property = property
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Rent(RentalAgreement rentalAgreement)
    {
        var property = await _context.Property.FindAsync(rentalAgreement.PropertyId);
        if (property == null || !property.IsAvailable)
        {
            return BadRequest("Lokal jest niedostępny.");
        }

        var userId = _userManager.GetUserId(User);

        // Sprawdź, czy najemca istnieje
        var tenant = await _context.Tenant.FirstOrDefaultAsync(t => t.UserId == userId);
        if (tenant == null)
        {
            tenant = new Tenant
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                FullName = User.Identity.Name ?? "Nieznany użytkownik",
                Email = (await _userManager.GetUserAsync(User))?.Email,
                PhoneNumber = "" // Możesz dodać logikę do pozyskiwania numeru
            };

            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();
        }

        // Tworzenie nowej umowy najmu
        rentalAgreement.TenantId = tenant.Id;
        rentalAgreement.StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
        rentalAgreement.MonthlyRent = property.RentPrice;

        property.IsAvailable = false; // Ustawienie lokalu jako zajętego

        _context.RentalAgreement.Add(rentalAgreement);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    


}