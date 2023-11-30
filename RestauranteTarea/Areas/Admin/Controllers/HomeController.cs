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
        private readonly Repository<Clasificacion> clasiRepo;

        public HomeController(Repository<Menu> RepoMenu, MenuRepository MenuRepo, Repository<Clasificacion> clasiRepo)
        {
            repoMenu = RepoMenu;
            menuRepo = MenuRepo;
            this.clasiRepo = clasiRepo;
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
                .Select(x => new ClasificacionHamburguesaModel()
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

        public IActionResult AgregarMenu()
        {
            AdminAddMenuViewModel vm = new();
            vm.Clasificaciones = clasiRepo.GetAll().OrderBy(x=> x.Nombre)
                .Select(x=> new ClasificacionModel()
                {
                    Id = x.Id,
                    Nombre=x.Nombre
                });
            return View(vm);
        }
        [HttpPost]
        public IActionResult AgregarMenu(AdminAddMenuViewModel vm)
        {

            if (string.IsNullOrEmpty(vm.Nombre))
            { 
                ModelState.AddModelError(string.Empty, "El nombre es requerido");
            }

            if (string.IsNullOrEmpty(vm.Descripcion))
            {
                ModelState.AddModelError(string.Empty, "La descripción es requerida");
            }

            if (vm.Precio == 0)
            {
                ModelState.AddModelError(string.Empty, "El precio no puede ser $0");
            }

            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permite imagenes JPEG");
                }

                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permite archivos no mayores a 500Kb");

                }
            }
            
            if (ModelState.IsValid)
            {
                var Hambubu = new Menu()
                {
                    Id = vm.Id,
                    Nombre = vm.Nombre,
                    Precio = (double)vm.Precio,
                    Descripción = vm.Descripcion,
                    IdClasificacion = vm.IdClasificacion
                };
                repoMenu.Insert(Hambubu);
                if (vm.Archivo == null) 
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{Hambubu.Id}.png");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{Hambubu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Menu");
            }
            vm.Clasificaciones = clasiRepo.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new ClasificacionModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
            return View(vm);
        }

        public IActionResult EditarMenu(int Id)
        {
            var Hambu = menuRepo.Get(Id);
            if(Hambu == null)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                AdminAddMenuViewModel vm = new();
                vm.Id = Hambu.Id;
                vm.Nombre = Hambu.Nombre;
                vm.Descripcion = Hambu.Descripción;
                vm.Precio = (decimal)Hambu.Precio;
                vm.IdClasificacion = Hambu.IdClasificacion;

                vm.Clasificaciones = clasiRepo.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new ClasificacionModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
                return View(vm);
            }
            
        }
        [HttpPost]
        public IActionResult EditarMenu(AdminAddMenuViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Nombre))
            {
                ModelState.AddModelError(string.Empty, "El nombre es requerido");
            }

            if (string.IsNullOrEmpty(vm.Descripcion))
            {
                ModelState.AddModelError(string.Empty, "La descripción es requerida");
            }

            if (vm.Precio == 0)
            {
                ModelState.AddModelError(string.Empty, "El precio no puede ser $0");
            }
             
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permite imagenes PNG");
                }

                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permite archivos no mayores a 500Kb");

                }
            }

            if (ModelState.IsValid)
            {
                var Hambubu = menuRepo.Get(vm.Id);
                if(Hambubu == null)
                {
                    RedirectToAction("Menu");
                }

                Hambubu.Nombre = vm.Nombre;
                Hambubu.Precio = (double)vm.Precio;
                Hambubu.Descripción = vm.Descripcion;
                Hambubu.IdClasificacion = vm.IdClasificacion;

                menuRepo.Update(Hambubu);
                if(vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{Hambubu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Menu");
            }
            vm.Clasificaciones = clasiRepo.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new ClasificacionModel()
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
            return View(vm);
        }

    }
}
