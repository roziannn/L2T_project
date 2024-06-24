using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("Read")]
    public class ReadController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetArticles")]
        public async Task<IActionResult> GetArticles()
        {
           
        var articles = await blogPostRepository.GetAllAsync();


            return Json(articles);
        }
    }
}
