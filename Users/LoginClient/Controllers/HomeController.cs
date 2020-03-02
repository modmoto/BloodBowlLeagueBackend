using Microsoft.AspNetCore.Mvc;

namespace LoginClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}