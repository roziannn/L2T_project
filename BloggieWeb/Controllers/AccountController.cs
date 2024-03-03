using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
