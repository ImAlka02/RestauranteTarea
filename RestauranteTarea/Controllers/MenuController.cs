using Microsoft.AspNetCore.Mvc;
using RestauranteTarea.Models.Entities;
using RestauranteTarea.Models.ViewModels;
using RestauranteTarea.Repositories;

namespace RestauranteTarea.Controllers
{
    public class MenuController : Controller
    {
        private readonly Repository<Menu> repo;
        public MenuController(Repository<Menu> repository)
        {
            MenuViewModel vm = new MenuViewModel()
            {
                Clasificaciones = repo.GetAll().GroupBy(x => x.IdClasificacionNavigation)
                .Select(x=> new ClasificacionViewModel()
                {
                    Nombre = x.Key.Nombre,
                    Hamburguesas = x.Select(x=> new HamburguesaModel()
                    {

                    }).ToList()
                })
            };
            
            
        }
        public IActionResult Menu()
        {

            return View();
        }
    }
}
