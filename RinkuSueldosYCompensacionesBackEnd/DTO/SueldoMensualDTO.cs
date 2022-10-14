using System;
using Microsoft.EntityFrameworkCore;
using RinkuSueldosYCompensacionesBackEnd.Context;
using RinkuSueldosYCompensacionesBackEnd.Interfaces;
using RinkuSueldosYCompensacionesBackEnd.Models;
using RinkuSueldosYCompensacionesBackEnd.Models.Helpers;

namespace RinkuSueldosYCompensacionesBackEnd.DTO
{
	public class SueldoMensualDTO : ISueldoMensualDTO
    {
        private readonly DataContext _context;
		private const int _diasMes = 4;
        public SueldoMensualDTO( DataContext dataContext )
		{
			_context = dataContext;

		}
        public async Task<IEnumerable<SueldoMensual>> GetSueldosMensualesAsync(FiltroSueldos filtro)
		{

			IEnumerable<Movimiento> movimientos = await (from _mov in _context.tblMovimientos
														 
														 where _mov.idMes == filtro.idMes &&
														 _mov.anio == filtro.anio
														 group _mov by new
														 {
															 _mov.idEmpleado,
															 _mov.idMes,
															 _mov.anio
														 }
														 into grupmov
														 select new Movimiento()
														 {
															 id=0,
															 idEmpleado=grupmov.First().idEmpleado,
															 idMes=grupmov.First().idMes,
															 anio=grupmov.First().anio,
															 numEntregas=grupmov.Sum(x=>x.numEntregas)
														 }
														 ).ToListAsync();


			List<int> idempleados = movimientos.Select(x=>x.idEmpleado).ToList();
            List<Empleado> empleados = await _context.tblEmpleados.Where(x => idempleados.Contains(x.id)).ToListAsync();
            List<EmpleadoRol> rols = await _context.tblEmpleadosRoles.ToListAsync();
			//IEnumerable<Retencion> retencions = await _context.tblRetenciones.ToListAsync();
			PorcentajeVale? porcentajevale  = await _context.tblPorcentajeVales.FirstOrDefaultAsync();

			List<SueldoMensual> sueldosMensuales = new(); 
			movimientos.ToList().ForEach(movimiento =>
			{
				SueldoMensual sueldoMensual = new();
				Empleado empleado = empleados.Find(x=>x.id==movimiento.idEmpleado)!;
				EmpleadoRol rol = rols.Find(x => x.id == empleado.rolId)!;

				decimal horasSemana = (rol.horasJornada ?? 0) * (rol.diasPorSemana ?? 0);
				decimal horasMes = horasSemana * _diasMes;

                sueldoMensual.idEmpleado = empleado.id;
				sueldoMensual.nombreEmpleado = empleado.nombre;
				sueldoMensual.totalEntregas = movimiento.numEntregas;
				sueldoMensual.sueldoBase = (rol.sueldoBase??0) * horasMes;
				sueldoMensual.bonoPorHora = (rol.montoBonoPorHora ?? 0) * horasMes;
				sueldoMensual.bonoPorEntrega = (rol.montoAdicionalPorEntrega ?? 0) * movimiento.numEntregas;

				decimal sueldobruto = sueldoMensual.sueldoBase + sueldoMensual.bonoPorHora + sueldoMensual.bonoPorEntrega;
				decimal porcentajeRet = sueldobruto < 10000 ? 9 : 11;

				sueldoMensual.totalRetencion = (porcentajeRet / 100 ) * sueldobruto;
				sueldoMensual.totalSueldo = sueldobruto - sueldoMensual.totalRetencion;
				sueldoMensual.montoVales = (porcentajevale!.porcentaje / 100) * sueldoMensual.totalSueldo;
                sueldosMensuales.Add(sueldoMensual);
            });
			return sueldosMensuales;

        }
	}
}

