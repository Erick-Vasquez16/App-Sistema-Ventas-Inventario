using Microsoft.AspNetCore.Mvc;

namespace InventarioVentas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
