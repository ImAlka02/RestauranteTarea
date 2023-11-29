namespace RestauranteTarea.Models.ViewModels
{
    public class MenuViewModel
    {
        public HamburguesaModel Hamburguesa { get; set; }
        public IEnumerable<ClasificacionViewModel> Clasificaciones { get; set; } = null!;
    }

    public class HamburguesaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Seleccionado { get; set; }
        public decimal Precio { get; set; }

    }
}
