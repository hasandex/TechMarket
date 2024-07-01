using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    [Authorize(Roles = ClsRoles.roleAdmin)]
    public class AdminController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        private readonly IOrderRepo _orderRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IProductRepo productRepo,
            IUserRepo userRepo,
            IUserService userService,
            IOrderRepo orderRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager )
        {
            _productRepo = productRepo;
            _userRepo = userRepo;
            _userService = userService;
            _orderRepo = orderRepo;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            //get all products
            var products = await _productRepo.GetAll();
            ViewBag.TotalUsers = _userRepo.Count();
            ViewBag.TotalProducts = await _productRepo.GetCountAllProducts();
            ViewBag.Products = "Products";
            return View(products);
        }
        public async Task<IActionResult> DisplayUsers()
        {
            var users = await _userRepo.GetUsers();
            ViewBag.Users = "Users";
            return View(users);
        }
        public async Task<IActionResult> DisplayOrders()
        {
            var orders = await _orderRepo.GetOrders();
            ViewBag.Orders = "Orders";
            return View(orders);
        }
        public async Task<IActionResult> DisplayOrderDetails(int orderId)
        {
            var order = await _orderRepo.GetOrderById(orderId);
            return View(order);
        }

        public IActionResult ChangeProductState(int productId)
        {
            var isChanged = _productRepo.ChangeProductState(productId);
            if (isChanged == -1)
            {
                return BadRequest();
            }
            return RedirectToAction("Details", "Product", new { id = productId });
        }

        [HttpPost]
        public IActionResult SubmitForm(string action, string? message, int productId)
        {
            //if (action == "Accept")
            //{
            //    // Handle Accept logic
            //}
            //else if (action == "Reject")
            //{
            //    // Handle Reject logic
            //}


            // Handle the message and productId as needed
            // ...
            var Done = _productRepo.DoSome(action,message,productId);
            if (Done == -1)
            {
                return BadRequest();
            }
            return RedirectToAction("Details", "Product", new { id = productId });
        }




        public async Task<IActionResult> MyProducts()
        {
            return View(await _productRepo.GetAll(_userService.GetUserId()));
        }
        public async Task<IActionResult> addRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);

            var allRoles = await _roleManager.Roles.ToListAsync();
            if (allRoles != null)
            {
                var roleList = allRoles.Select(r => new RoleViewModel()
                {
                    roleId = r.Id,
                    roleName = r.Name,
                    useRole = userRoles.Any(x => x == r.Name)
                });
                ViewBag.userName = user.UserName;
                ViewBag.userId = userId;
                return View(roleList);
            }
            else
                return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addRoles(string userId, string jsonRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            List<RoleViewModel> myRoles =
                JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonRoles);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in myRoles)
                {
                    if (userRoles.Any(x => x == role.roleName.Trim()) && !role.useRole)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.roleName.Trim());
                    }

                    if (!userRoles.Any(x => x == role.roleName.Trim()) && role.useRole)
                    {
                        await _userManager.AddToRoleAsync(user, role.roleName.Trim());
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }
    }
}
