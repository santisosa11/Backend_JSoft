using JSoft.Core.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSoft.Infraestructure.Data
{
    public class ServidoresRepository
    {
        private readonly string _connectionString;
        public ServidoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<Servidores>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetServidores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Servidores>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        private Servidores MapToValue(SqlDataReader reader)
        {
            return new Servidores()
            {
                idxServidores = (decimal)reader["IDX_SERVIDORES"],
                IPServidor = reader["IP_SERVIDOR"].ToString(),
                NombreServidor = reader["NOMBRE_SERVIDOR"].ToString(),
                idxCliente = (decimal)reader["IDX_CLIENTE"],
                Estado = (bool)reader["ESTADO"]
            };
        }

        public async Task InsertServidores(Servidores value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertServidores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IPServidor", value.IPServidor));
                    cmd.Parameters.Add(new SqlParameter("@NombreServidor", value.NombreServidor));
                    cmd.Parameters.Add(new SqlParameter("@IdxCliente", value.idxCliente));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task UpdateServidores(Servidores value, decimal id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateServidores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idxServidores", id));
                    cmd.Parameters.Add(new SqlParameter("@IPServidor", value.IPServidor));
                    cmd.Parameters.Add(new SqlParameter("@NombreServidor", value.NombreServidor));
                    cmd.Parameters.Add(new SqlParameter("@IdxCliente", value.idxCliente));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
