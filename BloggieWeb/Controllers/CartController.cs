using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
