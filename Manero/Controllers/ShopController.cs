using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Shop";
            return View();
        }
    }
}
