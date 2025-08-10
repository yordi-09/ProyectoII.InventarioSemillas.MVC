using System.ComponentModel.DataAnnotations;

namespace ProyectoII.InventarioSemillas.MVC.Models.Ubicaciones
{
    public class UbicacionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "Las condiciones de almacenamiento son requeridas")]
        [Display(Name = "Condiciones de almacenamiento")]
        public required string CondicionesAlmacenamiento { get; set; }
    }
}
