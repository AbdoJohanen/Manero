using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class WishController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ActivePage = "Wish";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
