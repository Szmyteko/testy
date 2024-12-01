using Microsoft.AspNetCore.Mvc;

namespace APT_Market.Controllers;

public class PropertyController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> MyListings()
    {
        return View();
    }
}