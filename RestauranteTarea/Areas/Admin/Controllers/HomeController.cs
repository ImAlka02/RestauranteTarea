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
        
        public IActionResult AgregarPromocion(int Id)
        {
            var dato = menuRepo.Get(Id);
            if(dato == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AddPromoAdminViewModel vm = new()
                {
                    Id = dato.Id,
                    Nombre = dato.Nombre,
                    Precio = (decimal)dato.Precio,
                    PrecioPromo = (decimal?)dato.PrecioPromocion
                };
                return View(vm);
            }
            
        }
        [HttpPost]
        public IActionResult AgregarPromocion(AddPromoAdminViewModel vm)
        {
            if (vm.PrecioPromo == 0)
            {
                ModelState.AddModelError(string.Empty, "El precio no puede ser $0.");
            }

            if (vm.PrecioPromo >= vm.Precio)
            {
                ModelState.AddModelError(string.Empty, "El precio de la promocion no puede ser mayor al original.");
            }

            if (ModelState.IsValid)
            {
                var Hamburguesa = menuRepo.Get(vm.Id);
                if (Hamburguesa == null)
                {
                    RedirectToAction("Index");
                }

                Hamburguesa.PrecioPromocion = (double?)vm.PrecioPromo;
                repoMenu.Update(Hamburguesa);
                return RedirectToAction("Menu");
            }
            return View(vm);
        }

        public IActionResult QuitarPromocion(int Id)
        {
            var dato = menuRepo.Get(Id);
            if (dato == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AddPromoAdminViewModel vm = new()
                {
                    Id = dato.Id,
                    Nombre = dato.Nombre,
                    Precio = (decimal)dato.Precio,
                    PrecioPromo = (decimal?)dato.PrecioPromocion
                };
                return View(vm);
            }
        }
        [HttpPost]
        public IActionResult QuitarPromocion(AddPromoAdminViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                var Hamburguesa = menuRepo.Get(vm.Id);
                if (Hamburguesa == null)
                {
                    RedirectToAction("Index");
                }

                Hamburguesa.PrecioPromocion = null;
                repoMenu.Update(Hamburguesa);
                return RedirectToAction("Menu");
            }
            return View(vm);
        }
    }
}
