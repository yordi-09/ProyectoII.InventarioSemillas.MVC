using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models.Ubicaciones;
using ProyectoII.InventarioSemillas.MVC.Servicios;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    [Authorize]
    public class UbicacionesController(UbicacionesServicio ubicacionesServicio) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var ubicaciones = await ubicacionesServicio.ObtenerTodasLasUbicaciones();
            return View(ubicaciones ?? []);
        }

        public IActionResult Create()
        {
            return View(new UbicacionDto
            {
                Nombre = "",
                Descripcion = "",
                CondicionesAlmacenamiento = ""
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UbicacionDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await ubicacionesServicio.CrearUbicacion(model);
                if (result)
                {
                    TempData["Success"] = "Ubicación creada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo crear la ubicación";
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ubicacion = await ubicacionesServicio.ObtenerUbicacionPorId(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UbicacionDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await ubicacionesServicio.ActualizarUbicacion(id, model);
                if (result)
                {
                    TempData["Success"] = "Ubicación actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "No se pudo actualizar la ubicación";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await ubicacionesServicio.EliminarUbicacion(id);
            if (result)
            {
                TempData["Success"] = "Ubicación eliminada exitosamente";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar la ubicación";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var ubicacion = await ubicacionesServicio.ObtenerUbicacionPorId(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }
    }
}
