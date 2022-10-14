using System;
using RinkuSueldosYCompensacionesBackEnd.Models.Helpers;

namespace RinkuSueldosYCompensacionesBackEnd.Interfaces
{
	public interface ISueldoMensualDTO
	{
        public Task<IEnumerable<SueldoMensual>> GetSueldosMensualesAsync(FiltroSueldos filtro);

    }
}

