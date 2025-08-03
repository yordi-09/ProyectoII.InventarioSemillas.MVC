using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoII.InventarioSemillas.MVC.Models.Roles;
using ProyectoII.InventarioSemillas.MVC.Servicios;

namespace ProyectoII.InventarioSemillas.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController(RolesServicio rolesServicio) : Controller
    {
        private readonly RolesServicio _rolesServicio = rolesServicio;

        public async Task<IActionResult> Index()
        {
            List<RolDto>? roles = await _rolesServicio.ObtenerTodosLosRoles();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rolesServicio.CrearRol(model);
                if (result != null)
                {
                    TempData["Success"] = "Rol creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string roleName)
        {
            var result = await _rolesServicio.EliminarRol(roleName);
            if (result)
            {
                TempData["Success"] = "Rol eliminado exitosamente";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar el rol";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AssignRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(UserRoleDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rolesServicio.AsignarRolAUsuario(model);
                if (result != null)
                {
                    TempData["Success"] = "Rol asignado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(UserRoleDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rolesServicio.RemoverRolDeUsuario(model);
                if (result != null)
                {
                    TempData["Success"] = "Rol removido exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["Error"] = "No se pudo remover el rol";
            return RedirectToAction(nameof(Index));
        }
    }
}