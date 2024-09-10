using InventarioVentas.Data;
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioVentas.Controllers
{
    public class ProductoController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public ProductoController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Producto> lista = await _appDbContext.Productos.ToListAsync();
            return View(lista);
        }
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
