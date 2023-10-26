using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class TrackController : Controller
{
    [HttpGet]
    public ActionResult Index(string order)
    {
        ViewBag.OrderNumber = order;
        return View();
    }
}
