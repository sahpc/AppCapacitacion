using System.ComponentModel.DataAnnotations;

namespace AppCapacitacion.Models
{
    public class Evaluacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un empleado.")]
        [Display(Name = "Empleado")]
        public int EmpleadoId { get; set; }

        public Empleado? Empleado { get; set; } 

        [Required(ErrorMessage = "Debes seleccionar una capacitación.")]
        [Display(Name = "Capacitación")]
        public int CapacitacionId { get; set; }

        public Capacitacion? Capacitacion { get; set; } 

        [Required(ErrorMessage = "La calificación es obligatoria.")]
        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10.")]
        public decimal Calificacion { get; set; }

        [MaxLength(500, ErrorMessage = "El comentario no debe exceder 500 caracteres.")]
        public string Comentario { get; set; }
    }
}
