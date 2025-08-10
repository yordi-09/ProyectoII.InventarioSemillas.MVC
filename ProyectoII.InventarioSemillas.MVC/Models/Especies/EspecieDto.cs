using System.ComponentModel.DataAnnotations;

namespace ProyectoII.InventarioSemillas.MVC.Models.Especies
{
    public class EspecieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre científico es requerido")]
        [Display(Name = "Nombre científico")]
        public required string NombreCientifico { get; set; }

        [Required(ErrorMessage = "El nombre común es requerido")]
        [Display(Name = "Nombre común")]
        public required string NombreComun { get; set; }

        [Required(ErrorMessage = "La familia es requerida")]
        [Display(Name = "Familia")]
        public required string Familia { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public required string Descripcion { get; set; }
    }
}
