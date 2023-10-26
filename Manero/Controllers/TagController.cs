using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.PortableExecutable;
using System;

namespace Manero.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BestSellers()
        {
            return View();
        }

        public IActionResult Featured()
        {
            return View();
        }
    }
}