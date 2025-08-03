using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models;
using ProyectoII.InventarioSemillas.MVC.Servicios;
using System.Diagnostics;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    public class HomeController(IApiCliente apiCliente) : Controller
    {
        private readonly IApiCliente _apiCliente = apiCliente;

        public IActionResult Index()
        {
            //var datos = await _apiCliente.ObtenerPronosticoAsync();
            //if (datos == null)
            //{
            //    TempData["Error"] = "Token inválido o expirado. Inicie sesión.";
            //    return RedirectToAction("Login", "Cuenta");
            //}

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
