using RestauranteTarea.Models.Entities;
using RestauranteTarea.Models.ViewModels;

namespace RestauranteTarea.Areas.Admin.Models
{
    public class MenuAdminViewModel
    {
        public IEnumerable<ClasificacionHamburguesaModel> Clasificaciones { get; set; } = null!;
    }

    public class ClasificacionHamburguesaModel
    {
        public string Nombre { get; set; } = null!;
        public IEnumerable<Menu> Hamburguesas { get; set; } = null!; 
    }
}
