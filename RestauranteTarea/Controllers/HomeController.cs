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
            var ListaPromo = menuRepository.GetAll().Where(x => x.PrecioPromocion != null && x.Id != Id);
            var next = ListaPromo.Skip(1).Take(1).First();
            var last = ListaPromo.Last();

            if(Id == 0)
            {
                
                var lol = menuRepository.GetAll().Where(x => x.PrecioPromocion != null).FirstOrDefault();
                vm.Id = lol.Id;
                vm.Nombre = lol.Nombre;
                vm.PrecioPromo = (decimal?)lol.PrecioPromocion;
                vm.Precio = (decimal)lol.Precio;
                vm.Descripcion = lol.Descripción;
                vm.IdPrev = last.Id;
                vm.IdNext = next.Id;
                return View(vm);
            }
            var Este = menuRepository.Get(Id);
            vm.Id = Este.Id;
            vm.Nombre = Este.Nombre;
            vm.PrecioPromo = (decimal?)Este.PrecioPromocion;
            vm.Precio = (decimal)Este.Precio;
            vm.Descripcion = Este.Descripción;
            vm.IdPrev = last.Id;
            vm.IdNext = next.Id;



            return View(vm);
        }
    }
}
