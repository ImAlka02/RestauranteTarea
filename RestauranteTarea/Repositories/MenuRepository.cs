using Microsoft.EntityFrameworkCore;
using RestauranteTarea.Models.Entities;

namespace RestauranteTarea.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
            
        }
        public override IEnumerable<Menu> GetAll()
        {
            return Context.Menu.Include(x => x.IdClasificacionNavigation)
                .OrderBy(x => x.Nombre);
        }
        public Menu? GetByNombre(string nombre)
        {
            return Context.Menu.Include(x=>x.IdClasificacionNavigation).FirstOrDefault(x=>x.Nombre == nombre);
        }
    }
}
