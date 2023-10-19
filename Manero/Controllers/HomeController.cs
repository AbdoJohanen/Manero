using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Home";
            return View();
        }
    }
}
