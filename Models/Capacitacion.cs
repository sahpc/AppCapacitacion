using System.ComponentModel.DataAnnotations;

namespace AppCapacitacion.Models
{
    public class Capacitacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un instructor.")]
        [Display(Name = "Instructor")] // 👈 importante
        public int InstructorId { get; set; }

        public Instructor? Instructor { get; set; } // 👈 sin Required aquí
    }
}
