using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryPaisValue
	{
		private readonly string _connectionString;
		public RepositoryPaisValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
        public async Task<List<PaisModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spPais_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<PaisModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValuePais(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(PaisModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spPais_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_CODIGO", model.Codigo));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PAIS", model.Id_pais));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					var response = new List<PaisModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private PaisModel MapToValuePais(SqlDataReader reader)
		{
			return new PaisModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Codigo = reader["CODIGO"].ToString(),
				Id_pais = Convert.ToInt32(reader["ID_PAIS"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nombre = reader["NOMBRE"].ToString()
			};
		}
	}

}
