using System.ComponentModel.DataAnnotations;

namespace ProyectoII.InventarioSemillas.MVC.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
