using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class BlogsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string urlHandle)
        {
            return View();
        }
    }
}
