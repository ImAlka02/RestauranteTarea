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

        public IActionResult Promociones(int Id)
        {
            PromoViewModel vm = new PromoViewModel();
            if(Id == 0)
            {
                vm.Promos = (IEnumerable<PromoModel>)menuRepository.GetAll().Where(x=> x.PrecioPromocion != null)
                    .Select(x=>new PromoModel()
                    {
                        Id =x.Id,
                        Descripcion = x.Descripción,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio,
                        PrecioPromo = (decimal?)x.PrecioPromocion
                    }).ToList();
                vm.Promocion = vm.Promos.FirstOrDefault();
                vm.IdPrev = vm.Promos.Skip(1).FirstOrDefault().Id;
                vm.IdPrev = vm.Promos.SkipLast(1).FirstOrDefault().Id;
            }
            return View(vm);
        }
    }
}
