using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models;
using System.Diagnostics;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
