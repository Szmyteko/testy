using APT_Market.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APT_Market.Controllers;

public class TenantController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public TenantController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [Authorize(Roles = "Najemca, Admin")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var rentalAgreements = await _context.RentalAgreement
            .Where(ra => ra.UserId == userId)
            .Include(ra => ra.Property)
            .ToListAsync();

        return View(rentalAgreements);
    }
}