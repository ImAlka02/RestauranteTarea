using RestauranteTarea.Models.Entities;

namespace RestauranteTarea.Models.ViewModels
{
    public class ClasificacionViewModel
    {
        public string Nombre { get; set; } = null!;
        public IEnumerable<Menu> Hamburguesas { get; set; } = null!;
    }

}
