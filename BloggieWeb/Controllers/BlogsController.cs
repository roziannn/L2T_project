using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class BlogsController : Controller

    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository) // inject the repository
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            return View(blogPost);
        }

    }
}
