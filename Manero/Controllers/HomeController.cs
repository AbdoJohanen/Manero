using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Manero.ViewModels.BackOffice;
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
