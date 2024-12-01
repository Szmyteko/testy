using Microsoft.AspNetCore.Mvc;

namespace APT_Market.Controllers;

public class TenantController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}