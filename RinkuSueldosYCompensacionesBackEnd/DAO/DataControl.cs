using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RinkuSueldosYCompensacionesBackEnd.DAO
{
	public class DataControl
	{
		private string _conn { get; set; }
        public DataControl(string conn)
		{
			_conn = conn;
		}

		public async Task ExecProcedureOneWay(string storeName, SqlParameter[] parameters)
		{
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(storeName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.AddRange(parameters);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<DataTable> ExecProcedureTwoWay(string storeName, SqlParameter[] parameters)
        {
            DataTable dataTable = new();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                SqlDataAdapter cmd = new SqlDataAdapter(storeName, conn);
                cmd.SelectCommand.CommandTimeout = 3600;
                cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.SelectCommand.Parameters.AddRange(parameters);
                await Task.Run(() => cmd.Fill(dataTable));
            }
            return dataTable;
        }
        
    }
}

