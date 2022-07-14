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
    public class ServiciosRepository
    {
        private readonly string _connectionString;
        public ServiciosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<Servicios>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetServicios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Servicios>();
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

        public async Task<Servicios> GetServiciosById(decimal id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetServiciosByID", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idxServidor", id));
                    Servicios response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        private Servicios MapToValue(SqlDataReader reader)
        {
            return new Servicios()
            {
                idxServicios = (decimal)reader["IDX_SERVICIOS"],
                idxServidor = (decimal)reader["IDX_SERVIDOR"],
                NombreServicio = reader["NOMBRE_SERVICIO"].ToString(),
                Capacidad = (decimal)reader["CAPACIDAD"],
                UmbralAlerta = (decimal)reader["UMBRAL_ALERTA"],
                Estado = (bool)reader["ESTADO"]
            };
        }

        public async Task InsertServicios(Servicios value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertServicios", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdxServidor", value.idxServidor));
                    cmd.Parameters.Add(new SqlParameter("@NombreServicio", value.NombreServicio));
                    cmd.Parameters.Add(new SqlParameter("@capacidad", value.Capacidad));
                    cmd.Parameters.Add(new SqlParameter("@UmbralAlerta", value.UmbralAlerta));
                    cmd.Parameters.Add(new SqlParameter("@Estado", value.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
