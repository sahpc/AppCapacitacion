using System.ComponentModel.DataAnnotations;

namespace AppCapacitacion.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Especialidad { get; set; }
    }
}
