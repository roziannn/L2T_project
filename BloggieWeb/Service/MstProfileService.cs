using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{
    public interface IMstProfileService
    {
        bool Update(Profile model);
    }


    public class MstProfileService(BloggieDbContext dbContext) : IMstProfileService
    {
        private readonly BloggieDbContext _context = dbContext;

        public bool Update(Profile model)
        {
            //var product = _context.Products.Find(model.Id);

            //if (product == null)
            //{
            //    return false;
            //}

            //product.ProductName = model.ProductName;
            //product.ProductCategory = model.ProductCategory;
            //product.Description = model.Description;
            //product.NormalPrice = model.NormalPrice;
            //product.Discount = model.Discount;
            //product.Stock = model.Stock;
            //product.ImageUrl = model.ImageUrl;
            //product.UrlHandle = model.UrlHandle;

            var profile = _context.Profiles.Find(model.UserNewId);

            if (profile == null)
            {
                return false;
            }

            profile.Email = model.Email;
            profile.FullName = model.FullName;
            profile.PhoneNumber = model.PhoneNumber;

            _context.Profiles.Update(profile);
            _context.SaveChanges();

            return true;
        }
    }
}
