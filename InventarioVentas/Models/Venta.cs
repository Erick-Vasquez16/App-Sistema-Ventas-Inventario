namespace InventarioVentas.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string NombreProducto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        //relacion
        public int IdProducto { get; set; }
        public Producto producto { get; set; }
    }
}
