using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechMarket.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IUserService _userService;
        private readonly string _userId;
        public OrderController(IOrderRepo orderRepo,IUserService userService)
        {
            _orderRepo = orderRepo;
            _userService = userService;
            _userId = _userService.GetUserId();
            if (string.IsNullOrEmpty(_userId))
            {
                return;
                //throw new UnauthorizedAccessException("You must be logged in to perform this action");
            }
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepo.GetOrders(_userId);
            return View(orders);
        }
        public async Task<IActionResult> Details(int orderId)
        {
            var order = await _orderRepo.GetOrderById(orderId);
            return View(order);
        }
    }
}
