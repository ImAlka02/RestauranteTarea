using Microsoft.AspNetCore.Mvc;
using RestauranteTarea.Models.Entities;
using RestauranteTarea.Models.ViewModels;
using RestauranteTarea.Repositories;

namespace RestauranteTarea.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(Repository<Menu> RepoMenu, MenuRepository menuRepository)
        {
            repoMenu = RepoMenu;
            this.menuRepository = menuRepository;
        }
        public Repository<Menu> repoMenu { get;}
        public MenuRepository menuRepository { get; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu(string Id)
        {
            if(Id != null)
            {
                Id = Id.Replace("-", " ");
            }
            MenuViewModel vm = new MenuViewModel()
            {
                Hamburguesa = menuRepository.GetByNombre(Id) ?? menuRepository.GetAll().OrderBy(x => x.Nombre).First(),
                Tipos = menuRepository.GetAll().GroupBy(x => x.IdClasificacionNavigation)
                .Select(x => new TipoModel()
                {
                    Tipo = x.Key.Nombre,
                    Hamburguesas = x.Where(y => y.IdClasificacion == x.Key.Id).Select(h => new HamburguesaModel()
                    {
                        Id = h.Id,
                        Nombre = h.Nombre,
                        Precio = (decimal)h.Precio
                    }).ToList()
                })
            };
           
            return View(vm);
        }

        public IActionResult Promociones()
        {
            return View();
        }
    }
}
