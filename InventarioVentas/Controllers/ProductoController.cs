using InventarioVentas.Data;
using InventarioVentas.Models;
using InventarioVentas.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioVentas.Controllers
{
    [Authorize]

    public class ProductoController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public ProductoController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Lista(string? nombreProducto, int page = 1, int pageSize = 10)
        {
            var viewModel = await ObtenerProductosConPaginacionYFiltrado(nombreProducto, page, pageSize);
            return View(viewModel);
        }

        // Método privado para obtener productos con filtrado y paginación
        private async Task<ProductosConPaginacionViewModel> ObtenerProductosConPaginacionYFiltrado(string? nombreProducto, int page = 1, int pageSize = 5)
        {
            var query = _appDbContext.Productos.AsQueryable();

            // Filtrado por nombre de producto
            if (!string.IsNullOrEmpty(nombreProducto))
            {
                query = query.Where(p => p.Nombre.Contains(nombreProducto));
            }

            // Conteo total para la paginación
            var totalItems = await query.CountAsync();

            // Paginación
            var productos = await query
                .OrderBy(p => p.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Creación del ViewModel
            return new ProductosConPaginacionViewModel
            {
                Productos = productos,
                Pagination = new PaginationViewModel
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalItems
                },
                NombreProductoFiltro = nombreProducto
            };
        }

        ///--------------------------------------------------
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Nuevo(Producto producto)
        {
            await _appDbContext.Productos.AddAsync(producto);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
        //para editar
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Producto producto = await _appDbContext.Productos.FirstAsync(e => e.IdProducto == id);

            return View(producto);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto)
        {
            _appDbContext.Productos.Update(producto);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
        //para eliminar empleado
        public async Task<IActionResult> Eliminar(int id)
        {
            Producto producto = await _appDbContext.Productos.FirstAsync(e => e.IdProducto == id);

            _appDbContext.Productos.Remove(producto);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
