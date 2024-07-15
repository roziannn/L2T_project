using BloggieWeb.Data;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("MstArticle")]
    [Authorize(Roles = "Admin")]

    public class MstArticleController(IMstArticleService articleService, BloggieDbContext dbContext) : Controller
    {
        private readonly IMstArticleService _articleService = articleService;
        private readonly BloggieDbContext _context = dbContext;

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _articleService.GetAll();
            return Ok(products);
        }
    }
}
