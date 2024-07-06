using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IRatingRepo _ratingRepo;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger,
            IProductRepo productRepo,
            ICategoryRepo categoryRepo,
            IRatingRepo ratingRepo,
            IEmailSender emailSernder)
        {
            _logger = logger;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _ratingRepo = ratingRepo;
            _emailSender = emailSernder;
        }

        public async Task<IActionResult> Stock(string? searchName, string? categoryName, int pg = 1)
        {
            var products = await _productRepo.GetAllAvailable();
            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchName.ToLower())
                    || p.Description.ToLower().Contains(searchName.ToLower())).ToList();
            }
            else if (categoryName != null)
            {
                products = products.Where(p => p.Category.Name.ToLower() == categoryName.ToLower()).ToList();
            }
            ViewBag.seachName = searchName;
            ViewBag.categories = _categoryRepo.GetCategories();
            //Pagenatio
            const int pageSize = 8;
            if (pg < 1) { pg = 1; }
            int rescCount = products.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
            var indexProducts = data.Select(d=> new ProductIndexViewModel
            {
                product = d,
                ProductRate = _ratingRepo.GetProductRate(d.Id)
            } );
            this.ViewBag.Pager = pager;
            //get the number of products which still in "To Be Determided" Status to dipslay in the nav
            ViewBag.Count = await _productRepo.GetCountAllNewProducts();
            return View(indexProducts);
        }
        public IActionResult HomePage()
        {
            ViewBag.categories = _categoryRepo.GetCategories();
            return View();
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
        [HttpGet]
        public IActionResult ContactMail()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactMail(Contact contact)
        {
            return View();
        }
    }
}
