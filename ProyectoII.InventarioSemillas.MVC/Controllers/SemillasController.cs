using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models.Semillas;
using ProyectoII.InventarioSemillas.MVC.Models.Especies;
using ProyectoII.InventarioSemillas.MVC.Models.Ubicaciones;
using ProyectoII.InventarioSemillas.MVC.Servicios;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    [Authorize]
    public class SemillasController : Controller
    {
        private readonly SemillasServicio _semillasServicio;
        private readonly EspeciesServicio _especiesServicio;
        private readonly UbicacionesServicio _ubicacionesServicio;

        public SemillasController(SemillasServicio semillasServicio, EspeciesServicio especiesServicio, UbicacionesServicio ubicacionesServicio)
        {
            _semillasServicio = semillasServicio;
            _especiesServicio = especiesServicio;
            _ubicacionesServicio = ubicacionesServicio;
        }

        public async Task<IActionResult> Index(int? especieId)
        {
            var semillas = await _semillasServicio.ObtenerTodasLasSemillas();
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();

            var semillasPorEspecie = semillas?.GroupBy(s => s.EspecieId)
                                             .ToDictionary(g => g.Key, g => g.Count());
            ViewBag.Especies = especies ?? [];
            ViewBag.Ubicaciones = ubicaciones ?? [];
            ViewBag.EspecieId = especieId;
            ViewBag.SemillasPorEspecie = semillasPorEspecie ?? [];

            if (especieId.HasValue)
            {
                semillas = semillas?.Where(s => s.EspecieId == especieId.Value).ToList();
                ViewBag.EspecieSeleccionada = especies?.FirstOrDefault(e => e.Id == especieId.Value);
            }

            return View(semillas ?? []);
        }

        public async Task<IActionResult> Create()
        {
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();
            ViewBag.Especies = especies ?? [];
            ViewBag.Ubicaciones = ubicaciones ?? [];
            return View(new SemillaDto
            {
                Nombre = "",
                Cantidad = 0,
                EspecieId = especies?.FirstOrDefault()?.Id ?? 0,
                UbicacionId = ubicaciones?.FirstOrDefault()?.Id ?? 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SemillaDto model)
        {
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();
            ViewBag.Especies = especies ?? new List<EspecieDto>();
            ViewBag.Ubicaciones = ubicaciones ?? new List<UbicacionDto>();
            if (ModelState.IsValid)
            {
                var result = await _semillasServicio.CrearSemilla(model);
                if (result)
                {
                    TempData["Success"] = "Semilla creada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo crear la semilla";
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var semilla = await _semillasServicio.ObtenerSemillaPorId(id);
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();
            ViewBag.Especies = especies ?? new List<EspecieDto>();
            ViewBag.Ubicaciones = ubicaciones ?? new List<UbicacionDto>();
            if (semilla == null)
            {
                return NotFound();
            }
            return View(semilla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SemillaDto model)
        {
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();
            ViewBag.Especies = especies ?? new List<EspecieDto>();
            ViewBag.Ubicaciones = ubicaciones ?? new List<UbicacionDto>();
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _semillasServicio.ActualizarSemilla(id, model);
                if (result)
                {
                    TempData["Success"] = "Semilla actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo actualizar la semilla";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _semillasServicio.EliminarSemilla(id);
            if (result)
            {
                TempData["Success"] = "Semilla eliminada exitosamente";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar la semilla";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var semilla = await _semillasServicio.ObtenerSemillaPorId(id);
            var especies = await _especiesServicio.ObtenerTodasLasEspecies();
            var ubicaciones = await _ubicacionesServicio.ObtenerTodasLasUbicaciones();
            ViewBag.Especies = especies ?? new List<EspecieDto>();
            ViewBag.Ubicaciones = ubicaciones ?? new List<UbicacionDto>();
            if (semilla == null)
            {
                return NotFound();
            }
            return View(semilla);
        }
    }
}