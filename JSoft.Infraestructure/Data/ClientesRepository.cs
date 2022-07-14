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
    public class ClientesRepository
    {
        private readonly string _connectionString;
        public ClientesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<Clientes>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetClientes", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Clientes>();
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

        private Clientes MapToValue(SqlDataReader reader)
        {
            return new Clientes()
            {
                idxClientes = (decimal)reader["IDX_CLIENTES"],
                NombreCliente = reader["NOMBRE_CLIENTE"].ToString(),
                Nit = reader["NIT"].ToString(),
                Estado = (bool)reader["ESTADO"]
            };
        }

        public async Task InsertClientes(Clientes value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertClientes", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", value.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@Nit", value.Nit));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task UpdateClientes(Clientes value, decimal id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateClientes", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idxClientes", id));
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", value.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@Nit", value.Nit));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
