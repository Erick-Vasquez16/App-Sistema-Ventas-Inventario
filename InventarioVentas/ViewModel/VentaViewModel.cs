using InventarioVentas.Models;

namespace InventarioVentas.ViewModel
{
    public class VentaViewModel
    {
        public List<Venta> Ventas { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
    }
}
