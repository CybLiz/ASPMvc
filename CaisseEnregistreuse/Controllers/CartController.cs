using CaisseEnregistreuse.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaisseEnregistreuse.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = GetUserId();
            var cart = _cartService.GetCartByUser(userId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var userId = GetUserId();
            _cartService.AddProduct(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            var userId = GetUserId();
            _cartService.ClearCart(userId);
            return RedirectToAction("Index");
        }
    }
}
