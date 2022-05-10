using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositorySucursalValue
	{
		private readonly string _connectionString;
		public RepositorySucursalValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<SucursalModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spSucursal_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<SucursalModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueSucursal(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(SucursalModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spSucursal_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_DIRECCION", model.Direccion));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PAIS", model.Id_pais));
					cmd.Parameters.Add(new SqlParameter("@p_ID_SUCURSAL", model.Id_sucursal));
					cmd.Parameters.Add(new SqlParameter("@p_LATITUD", model.Latitud));
					cmd.Parameters.Add(new SqlParameter("@p_LONGITUD", model.Longitud));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					var response = new List<SucursalModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private SucursalModel MapToValueSucursal(SqlDataReader reader)
		{
			return new SucursalModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Direccion = reader["DIRECCION"].ToString(),
				Id_pais = Convert.ToInt32(reader["ID_PAIS"]),
				Id_sucursal = Convert.ToInt32(reader["ID_SUCURSAL"]),
				Latitud = reader["LATITUD"].ToString(),
				Longitud = reader["LONGITUD"].ToString(),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nombre = reader["NOMBRE"].ToString()
			};
		}
	}

}
