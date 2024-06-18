using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechMarket.Controllers
{
    [Authorize(Roles = ClsRoles.roleAdmin)]
    public class AdminController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        public AdminController(IProductRepo productRepo, IUserRepo userRepo, IUserService userService)
        {
            _productRepo = productRepo;
            _userRepo = userRepo;
            _userService = userService;
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
            return RedirectToAction("DisplayProducts");
        }
        public IActionResult RejectProduct(int id)
        {
            var isDeleted = _productRepo.RejectProduct(id);
            if (isDeleted == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("DisplayProducts");
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> DisplayProducts()
        {
            //ViewBag.Count = await _productRepo.GetCountAllNewProducts();
            var products = await _productRepo.GetAll();
            ViewBag.TotalUsers = _userRepo.Count();
            ViewBag.TotalProducts = await _productRepo.GetCountAllProducts();
            return View(products);
        }
        public async Task<IActionResult> DisplayUsers()
        {
            var users = await _userRepo.GetUsers();
            return View(users);
        }
        public async Task<IActionResult> MyProducts()
        {
            return View(await _productRepo.GetAll(_userService.GetUserId()));
        }
    }
}
