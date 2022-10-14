using Microsoft.Data.SqlClient;
using RinkuSueldosYCompensacionesBackEnd.Models;
using System.Data;

namespace RinkuSueldosYCompensacionesBackEnd.DAO
{
    public class MovimientoDAO
    {
        const string _storeName = "pMovimientos";
        private Movimiento _movimiento  { get; set; }
        private DataControl _dataControl { get; set; }
        public MovimientoDAO(string conn)
        {
            _dataControl = new(conn);
            _movimiento = new();
        }

        public async Task Create(Movimiento movimiento )
        {
            _movimiento = movimiento;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(1));
        }

        public async Task<IEnumerable<Movimiento>> GetAll()
        {
            return await GetMovimientosList(2);
        }

        public async Task<Movimiento?> FindById(int id)
        {
            _movimiento.id = id;
            IEnumerable<Movimiento> empleados = await GetMovimientosList(3);
            return empleados.FirstOrDefault();
        }

        public async void Update(Movimiento movimiento)
        {
            _movimiento = movimiento;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(4));
        }

        public async Task Delete(int id)
        {
            _movimiento.id = id;
            await _dataControl.ExecProcedureOneWay(_storeName, GetSqlParameters(5));
        }

        private async Task<IEnumerable<Movimiento>> GetMovimientosList(int storeCmd)
        {
            List<Movimiento> movimientos = new();
            DataTable movimientoslist = await _dataControl.ExecProcedureTwoWay(_storeName, GetSqlParameters(storeCmd));

            foreach (DataRow row in movimientoslist.Rows)
            {
                Movimiento movimiento = new();
                movimiento.id = (int)row["id"];
                movimiento.idEmpleado = (int)row["idEmpleado"];
                movimiento.numEntregas = (int)row["numEntregas"];
                movimiento.idMes = (int)row["idMes"];
                movimiento.anio = (int)row["anio"];
                movimientos.Add(movimiento);
            }
            return movimientos;
        }

        private SqlParameter[] GetSqlParameters(int storeCmd)
        {
            SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@cmd", SqlDbType.Int) { Value = storeCmd },
                    new SqlParameter("@id", SqlDbType.Int) { Value = _movimiento.id },
                    new SqlParameter("@idEmpleado", SqlDbType.Int) { Value = _movimiento.idEmpleado },
                    new SqlParameter("@numEntregas", SqlDbType.Int) { Value = _movimiento.numEntregas },
                    new SqlParameter("@idMes", SqlDbType.Int) { Value = _movimiento.idMes },
                    new SqlParameter("@anio", SqlDbType.Int) { Value = _movimiento.anio },
                };
            return sqlParameter;
        }
    }
}
