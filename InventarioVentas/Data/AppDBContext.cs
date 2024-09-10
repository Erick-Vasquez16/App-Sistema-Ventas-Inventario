using Microsoft.EntityFrameworkCore;
using InventarioVentas.Models;
namespace InventarioVentas.Data

{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base (options)
        {
            
        }

        public  DbSet<Usuario> Usuarios { get; set; }
        public  DbSet<Producto> Productos { get; set; }
        public  DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creando la tabla de usuarios
            modelBuilder.Entity<Usuario>(tb => {
                tb.HasKey(col => col.IdUsuario);
                tb.Property(col => col.IdUsuario).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.NombreCompleto).HasMaxLength(100);
                tb.Property(col => col.NombreUsuario).HasMaxLength(20);

            });

            modelBuilder.Entity<Usuario>().ToTable("Usuario");

            //creando la tabla de productos
            modelBuilder.Entity<Producto>(tb => {
                tb.HasKey(col => col.IdProducto);
                tb.Property(col => col.IdProducto).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre).HasMaxLength(100);
                tb.Property(col => col.Descripcion).HasMaxLength(200);
                tb.Property(col => col.Marca).HasMaxLength(50);
                tb.Property(col => col.Tipo).HasMaxLength(50);

            });

            modelBuilder.Entity<Producto>().ToTable("Producto");

            //creadno la tabla de ventas
            modelBuilder.Entity<Venta>(tb => {
                tb.HasKey(col => col.IdVenta);
                tb.Property(col => col.IdVenta)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.NombreProducto).HasMaxLength(300);

                //relacion de muchos a uno con grado
                tb.HasOne(v => v.producto)
                .WithMany(p => p.ventas)
                .HasForeignKey(v => v.IdProducto);

            });

            modelBuilder.Entity<Venta>().ToTable("Venta");
        }
    }
}
