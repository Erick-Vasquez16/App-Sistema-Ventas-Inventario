using InventarioVentas.Data;
using InventarioVentas.Extensions;
using InventarioVentas.Models;
using InventarioVentas.ViewModel;
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
        public async Task<IActionResult> Lista(DateOnly? fechaInicio, DateOnly? fechaFin, int page = 1, int pageSize = 10)
        {
            var viewModel = await ObtenerVentasConPaginacionYFiltrado(fechaInicio, fechaFin, page, pageSize);
            return View(viewModel);
        }
        // Método privado para obtener ventas con filtrado y paginación
        private async Task<VentaViewModel> ObtenerVentasConPaginacionYFiltrado(DateOnly? fechaInicio, DateOnly? fechaFin, int page, int pageSize)
        {
            var query = _appDbContext.Ventas.AsQueryable();

            // Filtrado por fecha usando DateOnly convertido a DateTime
            if (fechaInicio.HasValue)
            {
                var fechaInicioDateTime = fechaInicio.Value.ToDateTime();
                query = query.Where(v => v.Fecha >= fechaInicioDateTime);
            }

            if (fechaFin.HasValue)
            {
                var fechaFinDateTime = fechaFin.Value.ToDateTime().AddDays(1).AddTicks(-1); // Para incluir el final del día
                query = query.Where(v => v.Fecha <= fechaFinDateTime);
            }

            // Conteo total para la paginación
            var totalItems = await query.CountAsync();

            // Paginación
            var ventas = await query
                .OrderBy(v => v.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Creación del ViewModel
            return new VentaViewModel
            {
                Ventas = ventas,
                Pagination = new PaginationViewModel
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalItems
                },
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
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
