using System.ComponentModel.DataAnnotations;

namespace AppCapacitacion.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El puesto es obligatorio")]
        public string Puesto { get; set; }
    }
}
