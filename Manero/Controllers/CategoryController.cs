using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
