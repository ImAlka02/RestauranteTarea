using RestauranteTarea.Models.Entities;
using RestauranteTarea.Models.ViewModels;

namespace RestauranteTarea.Areas.Admin.Models
{
    public class MenuAdminViewModel
    {
        public IEnumerable<ClasificacionModel> Clasificaciones { get; set; } = null!;
    }

    public class ClasificacionModel
    {
        public string Nombre { get; set; } = null!;
        public IEnumerable<Menu> Hamburguesas { get; set; } = null!; 
    }
}
