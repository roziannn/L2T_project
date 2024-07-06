using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Controllers
{
    [Route("Cart")]

    public class CartController(ICartItemsService cartItemsService, UserManager<IdentityUser> userManager, BloggieDbContext dbContext) : Controller 
    {
        private readonly ICartItemsService _cartItemsService = cartItemsService;
        private readonly BloggieDbContext _context = dbContext;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItems cartItem)
        {
            string userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            // Get pengguna based UserId
            var user = await userManager.FindByIdAsync(userId);

            if (ModelState.IsValid)
            {
                var cartItemEntity = new CartItems
                {
                    Id = Guid.NewGuid(),
                    UserProfileId = userId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                };

                _context.CartItems.Add(cartItemEntity);
                _context.SaveChanges();

                return Ok(new { message = "Product added to cart successfully!" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            string userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cartItems = await _cartItemsService.GetAllCartItems(userId);

            return Ok(cartItems);
        }


        [HttpPost("DeleteItem")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var result = await _cartItemsService.DeleteCartItem(id);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Failed to delete item." });
            }
        }
    }


}
