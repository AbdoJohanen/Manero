using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
