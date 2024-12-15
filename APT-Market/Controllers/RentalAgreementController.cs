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
            return BadRequest("Lokal niedostępny.");
        }

        var userId = _userManager.GetUserId(User);

        // Sprawdź, czy Tenant istnieje
        var tenant = await _context.Tenant.FirstOrDefaultAsync(t => t.Id.ToString() == userId);
        if (tenant == null)
        {
            tenant = new Tenant
            {
                UserId = userId,
                FullName = User.Identity.Name ?? "Nieznany użytkownik",
                PhoneNumber = "", // Możesz pobrać numer z IdentityUser, jeśli dostępny
                Email = _userManager.GetUserName(User) // Zakładam, że UserName to email
            };

            _context.Tenant.Add(tenant);
            await _context.SaveChangesAsync();
        }

        // Tworzenie umowy najmu
        var rentalAgreement = new RentalAgreement
        {
            PropertyId = property.Id,
            TenantId = tenant.UserId, // Korzystamy z Tenant.Id
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            MonthlyRent = property.RentPrice
        };

        property.IsAvailable = false; // Ustawienie lokalu jako zajętego
        _context.RentalAgreement.Add(rentalAgreement);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


}