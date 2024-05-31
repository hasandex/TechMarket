using Microsoft.AspNetCore.Mvc;

namespace TechMarket.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepo _productRepo;
        public AdminController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Count = await _productRepo.GetCountAllNewProducts();
            var products = await _productRepo.GetAll();
            return View(products);
        }
        public IActionResult MakeProductAvailable(int id)
        {
            var isAdded = _productRepo.MakeAvailable(id);
            if(isAdded == -1)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        public IActionResult RejectProduct(int id)
        {
            var isDeleted = _productRepo.RejectProduct(id);
            if (isDeleted == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}
