using InventarioVentas.Models;

namespace InventarioVentas.ViewModel
{
    public class ProductosConPaginacionViewModel
    {
        public List<Producto> Productos { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public string? NombreProductoFiltro { get; set; }
    }
}
