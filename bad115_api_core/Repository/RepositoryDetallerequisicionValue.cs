using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryDetallerequisicionValue
	{
		private readonly string _connectionString;
		public RepositoryDetallerequisicionValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<DetallerequisicionModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDetallerequisicion_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<DetallerequisicionModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueDetallerequisicion(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(DetallerequisicionModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDetallerequisicion_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_CANTIDAD", model.Cantidad));
					cmd.Parameters.Add(new SqlParameter("@p_ESTADO", model.Estado));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PRODUCTO", model.Id_producto));
					cmd.Parameters.Add(new SqlParameter("@p_IDREQUISICION", model.Idrequisicion));
					var response = new List<DetallerequisicionModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private DetallerequisicionModel MapToValueDetallerequisicion(SqlDataReader reader)
		{
			return new DetallerequisicionModel
			{
				Cantidad = Convert.ToDecimal(reader["CANTIDAD"]),
				Estado = Convert.ToBoolean(reader["ESTADO"]),
				Id_producto = Convert.ToInt32(reader["ID_PRODUCTO"]),
				Idrequisicion = Convert.ToInt32(reader["IDREQUISICION"])
			};
		}
	}

}
