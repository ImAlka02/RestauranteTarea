namespace RestauranteTarea.Models.ViewModels
{
    public class PromoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal PrecioPromo { get; set; }

    }
}
