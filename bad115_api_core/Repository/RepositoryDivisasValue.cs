using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryDivisasValue
	{
		private readonly string _connectionString;
		public RepositoryDivisasValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<DivisasModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDivisas_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<DivisasModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueDivisas(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(DivisasModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDivisas_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_FACTOR_MONEDA", model.Factor_moneda));
					cmd.Parameters.Add(new SqlParameter("@p_ID_DIVISAS", model.Id_divisas));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PAIS", model.Id_pais));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADOR_EN", model.Modificador_en));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					cmd.Parameters.Add(new SqlParameter("@p_VALOR", model.Valor));
					var response = new List<DivisasModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private DivisasModel MapToValueDivisas(SqlDataReader reader)
		{
			return new DivisasModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Factor_moneda = Convert.ToDecimal(reader["FACTOR_MONEDA"]),
				Id_divisas = Convert.ToInt32(reader["ID_DIVISAS"]),
				Id_pais = Convert.ToInt32(reader["ID_PAIS"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Modificador_en = Convert.ToDateTime(reader["MODIFICADOR_EN"]),
				Nombre = reader["NOMBRE"].ToString(),
				Valor = Convert.ToDecimal(reader["VALOR"])
			};
		}
	}

}
