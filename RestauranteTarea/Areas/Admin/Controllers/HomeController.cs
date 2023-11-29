using Microsoft.AspNetCore.Mvc;
using RestauranteTarea.Areas.Admin.Models;
using RestauranteTarea.Models.Entities;
using RestauranteTarea.Repositories;

namespace RestauranteTarea.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Repository<Menu> repoMenu;
        private readonly MenuRepository menuRepo;

        public HomeController(Repository<Menu> RepoMenu, MenuRepository MenuRepo)
        {
            repoMenu = RepoMenu;
            menuRepo = MenuRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            MenuAdminViewModel vm = new MenuAdminViewModel()
            {
                Clasificaciones = menuRepo.GetAll().GroupBy(x => x.IdClasificacionNavigation)
                .Select(x => new ClasificacionModel()
                {
                    Nombre = x.Key.Nombre,
                    Hamburguesas = menuRepo.GetAll().Where(y => y.IdClasificacion == x.Key.Id)
                })
            };
            return View(vm);
        }
        
        public IActionResult AgregarPromocion()
        {
            return View();
        }
    }
}
