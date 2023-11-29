using Microsoft.AspNetCore.Mvc;

namespace RestauranteTarea.Controllers
{
    public class PromocionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
