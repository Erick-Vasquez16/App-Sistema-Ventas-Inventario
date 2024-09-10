namespace InventarioVentas.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }

        //relacion de uno a muchos
        public ICollection<Venta> ventas { get; set; }
    }
}
