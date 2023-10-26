using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ActivePage = "Home";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
