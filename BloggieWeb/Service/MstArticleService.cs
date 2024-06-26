using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{
    public interface IMstArticleService
    {
        Task<List<BlogPost>> GetAll();
    }
    public class MstArticleService(BloggieDbContext dbContext) : IMstArticleService
    {
        private readonly BloggieDbContext _context = dbContext;

        public async Task<List<BlogPost>> GetAll()
        {
            try
            {
                var articles = await _context.BlogPosts
                //.Where(n => n.IsActive == true)
                .Select(n => new BlogPost
                {
                    Id = n.Id,
                    Heading = n.Heading,
                    Tags = n.Tags,
                    Author = n.Author,
                    UrlHandle = n.UrlHandle,
                    Visible = n.Visible,
                    PublishedDate = n.PublishedDate
                })
                .ToListAsync();


                return articles;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch notifications.", ex);
            }
        }
    }
}
