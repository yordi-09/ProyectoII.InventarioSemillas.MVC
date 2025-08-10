using System.ComponentModel.DataAnnotations;

namespace ProyectoII.InventarioSemillas.MVC.Models.Semillas
{
    public class SemillaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public required string Nombre { get; set; }

        [Display(Name = "Especie")]
        [Required(ErrorMessage = "La especie es requerida")]
        public int EspecieId { get; set; }

        [Display(Name = "Ubicación")]
        public int UbicacionId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Fecha de almacenamiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaAlmacenamiento { get; set; }
    }
}