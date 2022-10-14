using System;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Data;
using RinkuSueldosYCompensacionesBackEnd.Models;

namespace RinkuSueldosYCompensacionesBackEnd.DAO
{
	public partial class EmpleadoDAO
	{
        const string _storeName = "pEmpleados";
        private Empleado _empleado { get; set; }
        private DataControl _dataControl { get; set; }
        public EmpleadoDAO(string conn)
        {
            _dataControl = new(conn);
            _empleado = new();
        }

        public async Task Create(Empleado empleado)
        {
            _empleado = empleado;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(1));
        }

        public async Task<IEnumerable<Empleado>> GetAll()
        {
            return await GetEmpleadoList(2);
        }

        public   async Task<Empleado?> FindById(int id)
        {
            _empleado.id = id;
            IEnumerable<Empleado> empleados = await GetEmpleadoList(3);
            return empleados.FirstOrDefault();
        }

        public async void Update(Empleado empleado)
        {
            _empleado = empleado;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(4));
        }

        public async Task Delete(int id)
		{
            _empleado.id = id;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(5));
        }

        private async Task<IEnumerable<Empleado>> GetEmpleadoList(int storeCmd)
        {
            List<Empleado> empleados = new();
            DataTable empleadoslist = await _dataControl.ExecProcedureTwoWay(_storeName, GetSqlParameters(storeCmd));
           
            foreach (DataRow row in empleadoslist.Rows)
            {
                Empleado empleado = new();
                empleado.id = (int)row["id"];
                empleado.rolId = (int)row["rolId"];
                empleado.numero = row["numero"].ToString();
                empleado.nombre = row["nombre"].ToString();
                empleados.Add(empleado);
            }
            return empleados;
        }

        private SqlParameter[] GetSqlParameters(int storeCmd)
        {
            SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@cmd", SqlDbType.Int) { Value = storeCmd },
                    new SqlParameter("@id", SqlDbType.Int) { Value = _empleado.id },
                    new SqlParameter("@rolId", SqlDbType.Int) { Value = _empleado.rolId },
                    new SqlParameter("@numero", SqlDbType.VarChar,200) { Value = _empleado.numero },
                    new SqlParameter("@nombre", SqlDbType.VarChar,200) { Value = _empleado.nombre },
                };
            return sqlParameter;
        }
	}
	
}

