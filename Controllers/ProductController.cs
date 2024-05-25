using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechMarket.Repo.IRepo;

namespace TechMarket.Controllers
{
    //[Authorize(Roles = $"{ClsRoles.roleUser},{ClsRoles.roleAdmin}")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IUserService _userService;
        public ProductController(IProductRepo productRepo, ICategoryRepo categoryRepo,IUserService userService)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.GetAll(_userService.GetUserId()));
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateProductViewModel()
            {
                CategoriesSelectList = _categoryRepo.GetCategoriesSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CategoriesSelectList = _categoryRepo.GetCategoriesSelectList();
                return View(viewModel);
            }
            var isCreated = _productRepo.Create(viewModel);
            if (isCreated == 0)
            {
                return BadRequest();
            }
            TempData["Success"] = "the item has been added successfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = _productRepo.GetById(id);
            if (item == null)
            {
                return BadRequest();
            }
            var viewModel = new UpdateProductViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
                CategoriesSelectList = _categoryRepo.GetCategoriesSelectList(),
                Cover = item.Cover
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Update(UpdateProductViewModel viewModel)
        {
            var item = _productRepo.GetById(viewModel.Id);
            if (item == null)
            {
                return BadRequest();
            }
            if (viewModel.FormFile == null)
            {
                ModelState.Remove("FormFile");
            }
            if (!ModelState.IsValid)
            {
                viewModel.CategoriesSelectList = _categoryRepo.GetCategoriesSelectList();
                return View(viewModel);
            }
            var isUpdated = _productRepo.Update(viewModel);
            if (isUpdated == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var isDeleted = _productRepo.Delete(id);
            if (isDeleted == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
