namespace RinkuSueldosYCompensacionesBackEnd.Models
{
    public class Movimiento
    {
        public int id { get; set; }
        public int idEmpleado { get; set; }
        public int numEntregas { get; set; }
        public int idMes { get; set; }
        public int anio { get; set; }
    }
}
