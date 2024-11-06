using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Web.Controllers
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
