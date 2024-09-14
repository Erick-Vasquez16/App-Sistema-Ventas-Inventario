using InventarioVentas.Models;

namespace InventarioVentas.ViewModel
{
    public class VentaTotalViewModel
    {
        public List<Venta> Ventas { get; set; } // Lista de ventas filtradas
        public DateTime? FechaInicio { get; set; } // Fecha de filtro
        public decimal TotalVendido { get; set; }
    }
}
