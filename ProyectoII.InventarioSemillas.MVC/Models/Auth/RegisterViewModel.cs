using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ProyectoII.InventarioSemillas.MVC.Models.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Confirmar contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public required string Apellido { get; set; }
        [Required(ErrorMessage = "El campo Rol es obligatorio.")]
        public required string Role { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
    }

}
