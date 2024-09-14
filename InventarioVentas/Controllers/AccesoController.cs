using Microsoft.AspNetCore.Mvc;
using InventarioVentas.Data;
using InventarioVentas.Models;
using Microsoft.EntityFrameworkCore;
using InventarioVentas.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace InventarioVentas.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public AccesoController(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;

        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioVM modelo)
        {
            if (modelo.Password != modelo.ConfirmarPassword)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View(modelo); // Devuelve el modelo para mantener los datos ingresados
            }

            // Encripta la contraseña utilizando BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(modelo.Password);

            Usuario usuario = new Usuario()
            {
                NombreCompleto = modelo.NombreCompleto,
                NombreUsuario = modelo.NombreUsuario,
                Password = hashedPassword, // Guarda la contraseña encriptada
            };

            await _appDbContext.Usuarios.AddAsync(usuario);
            await _appDbContext.SaveChangesAsync();

            if (usuario.IdUsuario != 0)
            {
                return RedirectToAction("Login", "Acceso");
            }

            ViewData["Mensaje"] = "Error al crear el usuario";
            return View(modelo); // Devuelve el modelo en caso de error
        }

        [HttpGet]
        public IActionResult Login()
        {
            //si el usuario esta activo en la sesion
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            // Buscar al usuario por nombre de usuario
            Usuario? usuario_encontrado = await _appDbContext.Usuarios
                .Where(u => u.NombreUsuario == modelo.NombreUsuario)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null || !BCrypt.Net.BCrypt.Verify(modelo.Password, usuario_encontrado.Password))
            {
                ViewData["Mensaje"] = "Nombre de usuario o contraseña incorrecta";
                return View();
            }

            // Si el usuario es encontrado y la contraseña es válida, configurar las claims
            List<Claim> claims = new List<Claim>() {
        new Claim(ClaimTypes.Name, usuario_encontrado.NombreCompleto)
    };

            // Refrescar la sesión automáticamente
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            // Firmar al usuario en el sistema
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Redirigir al Home después de autenticarse correctamente
            return RedirectToAction("Index", "Home");
        }
    }
}
