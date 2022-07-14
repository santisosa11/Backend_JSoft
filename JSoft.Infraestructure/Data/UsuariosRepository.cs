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
    public class UsuariosRepository
    {
        private readonly string _connectionString;
        public UsuariosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<Usuarios>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUsuarios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Usuarios>();
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

        private Usuarios MapToValue(SqlDataReader reader)
        {
            return new Usuarios()
            {
                idxUsuarios = (decimal)reader["IDX_USUARIOS"],
                Nombres = reader["NOMBRES"].ToString(),
                Usuario = reader["USUARIO"].ToString(),
                Contrasena = reader["CONTRASENA"].ToString(),
                Estado = (bool)reader["ESTADO"]
            };
        }

        public async Task InsertUsuarios(Usuarios value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUsuarios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@nombres", value.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@username", value.Usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", value.Contrasena));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task UpdateUsuarios(Usuarios value, decimal id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateUsuarios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idxUsuarios", id));
                    cmd.Parameters.Add(new SqlParameter("@nombres", value.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@username", value.Usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", value.Contrasena));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
