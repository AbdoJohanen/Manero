using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Login";
            return View();
        }
    }
}
