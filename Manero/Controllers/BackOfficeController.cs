using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers
{
    public class BackOfficeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
