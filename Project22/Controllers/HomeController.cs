using Microsoft.AspNetCore.Mvc;

namespace Project22.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
