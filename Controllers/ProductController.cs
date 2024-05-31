﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechMarket.Repo.IRepo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechMarket.Controllers
{
    //[Authorize(Roles = $"{ClsRoles.roleUser},{ClsRoles.roleAdmin}")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRatingRepo _ratingRepo;
        public ProductController(IProductRepo productRepo, ICategoryRepo categoryRepo,IUserService userService, UserManager<IdentityUser> userManager, IRatingRepo ratingRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _userService = userService;
            _userManager = userManager;
            _ratingRepo = ratingRepo;
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
            var usertRate = _ratingRepo.GetUserRate(_userService.GetUserId(),id);
            var productRate = _ratingRepo.GetProductRate(id);
            ViewBag.userRate = usertRate;
            ViewBag.productRate = productRate;
            return View(product);
        }
        public IActionResult RateProduct(string ratingValue,int productId)
        {
           var isRated = _ratingRepo.RateProduct(Int32.Parse(ratingValue),productId,_userService.GetUserId());
           if(isRated < 0)
            {
                return BadRequest();
            }
           return Ok();
        }
    }
}
