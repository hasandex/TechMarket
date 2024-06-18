using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;

namespace TechMarket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IProductRepo _productRepo;
        private readonly IUserService _userService;
        private readonly string _userId;
        public CartController(ICartRepo cartRepo, IProductRepo productRepo, IUserService userService, IOrderRepo orderRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _userService = userService;
            _userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(_userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
            _orderRepo = orderRepo;
        }
        public IActionResult Index()
        {
            var cart = _cartRepo.GetCartByUserId(_userId);

            if (cart == null)
            {
                return BadRequest();
            }
            return View(cart);


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
        public IActionResult RemoveFromCart(int productId)
        {
            var isDeleted = _cartRepo.RemoveFromCart(productId);
            if(isDeleted == -1)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            ViewBag.cartId = _cartRepo.GetCartByUserId(_userId).Id;
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }
            
            var isAdded = _orderRepo.Add(order);
            if(isAdded != 1)
            {
                return BadRequest();   
            }
            ViewData["orderIsAdded"] = "your order has been sent to the center";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                var cart = _cartRepo.GetCartByUserId(_userId);
                ViewData["ErrorMessage"] = "The minimum quantity for each product is 1";
                return View("Index", cart);
            }
            _cartRepo.UpdateCartContentQuantity(productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
