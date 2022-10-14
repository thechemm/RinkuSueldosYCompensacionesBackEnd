namespace RinkuSueldosYCompensacionesBackEnd.Models
{
    public class Retencion
    {
        public int id { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? montoMinimo { get; set; }
        public decimal? montoMaximo { get; set; }
    }
}
