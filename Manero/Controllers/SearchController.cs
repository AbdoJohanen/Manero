using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class SearchController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ActivePage = "Search";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string orderNumber)
    {
        return RedirectToAction("index", "track", new { order = orderNumber });
    }
}
