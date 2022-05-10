using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryEmpleadoValue
	{
		private readonly string _connectionString;
		public RepositoryEmpleadoValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<EmpleadoModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spEmpleado_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<EmpleadoModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueEmpleado(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(EmpleadoModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spEmpleado_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_DESERTO", model.Fecha_deserto));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_INICIO", model.Fecha_inicio));
					cmd.Parameters.Add(new SqlParameter("@p_IDEMPLEADO", model.Idempleado));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					var response = new List<EmpleadoModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private EmpleadoModel MapToValueEmpleado(SqlDataReader reader)
		{
			return new EmpleadoModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Fecha_deserto = Convert.ToDateTime(reader["FECHA_DESERTO"]),
				Fecha_inicio = Convert.ToDateTime(reader["FECHA_INICIO"]),
				Idempleado = Convert.ToInt32(reader["IDEMPLEADO"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"])
			};
		}
	}

}
