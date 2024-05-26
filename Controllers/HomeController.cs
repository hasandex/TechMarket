using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepo _productRepo;
        public readonly ICategoryRepo _categoryRepo;

        public HomeController(ILogger<HomeController> logger, IProductRepo productRepo, ICategoryRepo categoryRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index(string? seachName, string? categoryName, int pg = 1)
        {
            var products = await _productRepo.GetAllAvailable();
            if (!string.IsNullOrEmpty(seachName))
            {
                products = products.Where(p => p.Name.ToLower().Contains(seachName.ToLower())
                    || p.Description.ToLower().Contains(seachName.ToLower())).ToList();
            }
            else if (categoryName != null)
            {
                products = products.Where(p => p.Category.Name.ToLower() == categoryName.ToLower()).ToList();
            }
            ViewBag.seachName = seachName;
            ViewBag.categories = _categoryRepo.GetCategories();
            //Pagenatio
            const int pageSize = 8;
            if (pg < 1) { pg = 1; }
            int rescCount = products.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
