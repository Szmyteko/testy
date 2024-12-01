using Microsoft.AspNetCore.Mvc;

namespace APT_Market.Controllers;

public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}