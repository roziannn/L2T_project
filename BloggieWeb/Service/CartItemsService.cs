using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BloggieWeb.Service
{
    public interface ICartItemsService
    {
        Task<Product> Create(Product product);
        Task<List<CartItemsDto>> GetAllCartItems(string userId);
        Task<bool> DeleteCartItem(Guid id);


    }
    public class CartItemsService : ICartItemsService
    {
        private readonly BloggieDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartItemsService(BloggieDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<Product> Create(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCartItem(Guid id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return false;
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<CartItemsDto>> GetAllCartItems(string userId)
        {

            var cartItems = await _context.CartItems
               .Where(ci => ci.UserProfileId == userId)
               .Join(
                   _context.Products,
                   ci => ci.ProductId,
                   p => p.Id,
                   (ci, p) => new CartItemsDto
                   {
                       Id = ci.Id,
                       ProductId = ci.ProductId,
                       ImageUrl = p.ImageUrl,
                       ProductName = p.ProductName,
                       ProductCategory = p.ProductCategory,
                       Price = p.DiscountedPrice,
                       Quantity = ci.Quantity
                   }
               )
               .ToListAsync();

            return cartItems;
        }
    }
}
