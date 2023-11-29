namespace RestauranteTarea.Areas.Admin.Models
{
    public class AddPromoAdminViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal? PrecioPromo { get; set; }
    }
}
