using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models.Especies;
using ProyectoII.InventarioSemillas.MVC.Servicios;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    [Authorize]
    public class EspeciesController(EspeciesServicio especiesServicio) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var especies = await especiesServicio.ObtenerTodasLasEspecies();

            return View(especies ?? []);
        }

        public IActionResult Create()
        {
            List<object> familias = ObtenerListaDeFamilias();

            ViewBag.FamiliasDropdown = familias;

            return View(new EspecieDto
            {
                NombreCientifico = "",
                NombreComun = "",
                Familia = "",
                Descripcion = ""
            });
        }

        private static List<object> ObtenerListaDeFamilias()
        {
            return
            [
                new { Familia = "Fabaceae" },
                new { Familia = "Orchidaceae" },
                new { Familia = "Poaceae" },
                new { Familia = "Asteraceae" },
                new { Familia = "Cyperaceae" },
                new { Familia = "Bromeliaceae" },
                new { Familia = "Arecaceae" },
                new { Familia = "Rubiaceae" },
                new { Familia = "Melastomataceae" },
                new { Familia = "Meliaceae" }
            ];
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspecieDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await especiesServicio.CrearEspecie(model);
                if (result)
                {
                    TempData["Success"] = "Especie creada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo crear la especie";
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var especie = await especiesServicio.ObtenerEspeciePorId(id);
            List<object> familias = ObtenerListaDeFamilias();

            ViewBag.FamiliasDropdown = familias;

            if (especie == null)
            {
                return NotFound();
            }
            return View(especie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EspecieDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await especiesServicio.ActualizarEspecie(id, model);
                if (result)
                {
                    TempData["Success"] = "Especie actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo actualizar la especie";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await especiesServicio.EliminarEspecie(id);
            if (result)
            {
                TempData["Success"] = "Especie eliminada exitosamente";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar la especie";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var especie = await especiesServicio.ObtenerEspeciePorId(id);
            if (especie == null)
            {
                return NotFound();
            }
            return View(especie);
        }
    }
}
