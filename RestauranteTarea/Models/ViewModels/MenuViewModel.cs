using RestauranteTarea.Models.Entities;

namespace RestauranteTarea.Models.ViewModels
{
    public class MenuViewModel
    {
        public Menu Hamburguesa { get; set; } = null!;
        public IEnumerable<TipoModel> Tipos { get; set; } = null!;

    }

    public class TipoModel
    {
        public string Tipo { get; set; } = null!;
        public IEnumerable<HamburguesaModel> Hamburguesas { get; set; } = null!;
    }
    public class HamburguesaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
    }

    
}
