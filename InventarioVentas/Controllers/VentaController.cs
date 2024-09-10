using InventarioVentas.Data;
using InventarioVentas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioVentas.Controllers
{
    public class VentaController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public VentaController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Venta> lista = await _appDbContext.Ventas.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewBag.Producto = new SelectList(_appDbContext.Productos, "IdProducto", "Nombre");
            ViewBag.NombreProducto = new SelectList(_appDbContext.Productos, "Nombre", "Nombre");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Nuevo(Venta ventas)
        {
            await _appDbContext.Ventas.AddAsync(ventas);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
        //para editar
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.Producto = new SelectList(_appDbContext.Productos, "IdProducto", "Nombre");
            ViewBag.NombreProducto = new SelectList(_appDbContext.Productos, "Nombre", "Nombre");

            Venta venta = await _appDbContext.Ventas.FirstAsync(e => e.IdVenta == id);

            return View(venta);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Venta ventas)
        {
            _appDbContext.Ventas.Update(ventas);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        //para eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            Venta venta = await _appDbContext.Ventas.FirstAsync(e => e.IdVenta == id);

            _appDbContext.Ventas.Remove(venta);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
