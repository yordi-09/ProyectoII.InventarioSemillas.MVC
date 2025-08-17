using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoII.InventarioSemillas.MVC.Models.Auth;
using ProyectoII.InventarioSemillas.MVC.Models.Roles;
using ProyectoII.InventarioSemillas.MVC.Servicios;
using System.Security.Claims;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    public class AuthController(IHttpClientFactory httpClientFactory, RolesServicio rolesServicio) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly RolesServicio rolesServicio = rolesServicio;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("ApiJwt");
            var response = await client.PostAsJsonAsync("api/auth/Login", new
            {
                model.Email,
                model.Password
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                ModelState.AddModelError(string.Empty, "No se pudo obtener el token");
                return View(model);
            }

            await SignInUserAsync(model, result);

            return RedirectToAction("Index");
        }

        private async Task SignInUserAsync(LoginViewModel model, LoginResponseDto result)
        {
            HttpContext.Session.SetString("JWToken", result.Token);

            string nombreCompleto = ObtenerNombreCompleto(result.Name);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, nombreCompleto),
                new(ClaimTypes.Email, model.Email),
                new(ClaimTypes.Role, result.Rol),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        private static string ObtenerNombreCompleto(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var partes = name.Split('_');
            if (partes.Length != 2)
                return name;

            var nombre = char.ToUpper(partes[0][0]) + partes[0][1..].ToLower();
            var apellido = char.ToUpper(partes[1][0]) + partes[1][1..].ToLower();

            return $"{nombre} {apellido}";
        }

        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("JWToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<RolDto>? roles = await rolesServicio.ObtenerTodosLosRoles();
            var model = new RegisterViewModel
            {
                Email = "",
                Password = "",
                Nombre = "",
                Apellido = "",
                Role = "",
                Roles = roles?.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }) ?? []
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("ApiJwt");
            var response = await client.PostAsJsonAsync("api/auth/Registro", new
            {
                model.Email,
                model.Password,
                model.Nombre,
                model.Apellido,
                model.Role
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al registrar usuario.");
                return View(model);
            }

            return RedirectToAction("RegistroExitoso");
        }

        [HttpGet]
        public IActionResult RegistroExitoso()
        {
            return View();
        }
    }
}
