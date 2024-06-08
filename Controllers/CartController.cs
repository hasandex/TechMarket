using Microsoft.AspNetCore.Mvc;

namespace TechMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var isAdded = _cartRepo.AddToCart(productId);
            if (isAdded == 1)
            {
                return Json(new { success = "added" });
            }
            else if(isAdded == -1) // the product is already in your cart
            {
                return Json(new { success = "isExisted" });
            }
            return Json(new { success = "failed" });
        }
    }
}
