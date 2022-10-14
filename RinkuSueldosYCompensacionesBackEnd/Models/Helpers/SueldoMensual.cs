using System;
namespace RinkuSueldosYCompensacionesBackEnd.Models.Helpers
{
	public class SueldoMensual
	{
		public string? nombreEmpleado {get;set;}
		public int? idEmpleado { get;set;}
		public decimal totalEntregas { get;set;}
		public decimal sueldoBase { get;set;}
		public decimal bonoPorHora { get;set;}
		public decimal bonoPorEntrega { get;set;}
		public decimal totalRetencion { get;set;}
		public decimal totalSueldo { get;set;}
		public decimal montoVales { get;set;}
	}
}

