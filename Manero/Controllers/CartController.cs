using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ActivePage = "Cart";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
