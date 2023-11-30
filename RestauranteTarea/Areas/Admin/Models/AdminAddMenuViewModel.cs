using RestauranteTarea.Models.Entities;

namespace RestauranteTarea.Areas.Admin.Models
{
    public class AdminAddMenuViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdClasificacion { get; set; }
        public IEnumerable<ClasificacionModel>? Clasificaciones { get; set; }
        public IFormFile? Archivo {  get; set; }

    }
    public class ClasificacionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
