namespace RestauranteTarea.Models.ViewModels
{
    public class PromoViewModel
    {
        public PromoModel Promocion { get; set; }
        public int IdNext { get; set; }
        public int IdPrev { get; set; }
        public IEnumerable<PromoModel> Promos { get; set; } = null!;      
    }
    public class PromoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal? PrecioPromo { get; set; }
    }
}
