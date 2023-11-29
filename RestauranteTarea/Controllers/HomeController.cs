using Microsoft.AspNetCore.Mvc;

namespace RestauranteTarea.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
