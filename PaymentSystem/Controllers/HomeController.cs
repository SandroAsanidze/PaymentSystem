using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity?.Name;
            return View();
        }
    }
}
