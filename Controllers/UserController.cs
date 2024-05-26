using Microsoft.AspNetCore.Mvc;

namespace TechMarket.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
