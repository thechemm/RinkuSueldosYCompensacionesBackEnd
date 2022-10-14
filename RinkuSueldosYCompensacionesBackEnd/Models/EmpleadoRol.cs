namespace RinkuSueldosYCompensacionesBackEnd.Models
{
    public class EmpleadoRol
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public decimal? sueldoBase { get; set; }
        public decimal? horasJornada { get; set; }
        public int? diasPorSemana { get; set; }
        public decimal? montoAdicionalPorEntrega { get; set; }
        public decimal? montoBonoPorHora { get; set; }
    }
}
