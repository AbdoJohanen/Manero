using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class WishController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Wish";
            return View();
        }
    }
}
