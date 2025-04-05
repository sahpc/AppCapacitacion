namespace AppCapacitacion.Models
{
    public class EmpleadoCapacitacion
    {
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public int CapacitacionId { get; set; }
        public Capacitacion Capacitacion { get; set; }
    }
}
