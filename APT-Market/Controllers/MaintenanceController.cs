using Microsoft.AspNetCore.Mvc;

namespace APT_Market.Controllers;

public class MaintenanceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}