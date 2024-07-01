using Microsoft.AspNetCore.Mvc;
using TechMarket.Models;
using TechMarket.Repo;

namespace TechMarket.Controllers
{
    public class UserController : Controller
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IRatingRepo _ratingRepo;
        private readonly IUserService _userService;
        public UserController(ICommentRepo commentRepo, IRatingRepo ratingRepo, IUserService userService)
        {
            _commentRepo = commentRepo;
            _ratingRepo = ratingRepo;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment([Bind("Id,Content,UserId,ProductId")] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            _commentRepo.Add(comment);
            return RedirectToAction("Details", "Product", new { id = comment.ProductId });
        }
        public IActionResult RemoveComment(int commentId)
        {
            _commentRepo.RemoveComment(commentId);
            return RedirectToAction("Details", "Product", new { id = commentId });
        }
        public IActionResult RateProduct(string rating, int productId)
        {
            var isRated = _ratingRepo.RateProduct(Int32.Parse(rating), productId, _userService.GetUserId());
            if (isRated < 0)
            {
                return BadRequest();
            }
            int id = productId;
            return RedirectToAction("Details","Product", new { id = productId });
        }
    }
}
