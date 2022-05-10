using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryProveedorValue
	{
		private readonly string _connectionString;
		public RepositoryProveedorValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
        public async Task<List<ProveedorModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spProveedor_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<ProveedorModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueProveedor(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(ProveedorModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spProveedor_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_CONTACTO_VENTAS", model.Contacto_ventas));
					cmd.Parameters.Add(new SqlParameter("@p_CORREO", model.Correo));
					cmd.Parameters.Add(new SqlParameter("@p_DIRECCION", model.Direccion));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PROVEEDOR", model.Id_proveedor));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					cmd.Parameters.Add(new SqlParameter("@p_NRC", model.Nrc));
					cmd.Parameters.Add(new SqlParameter("@p_TELEFONO", model.Telefono));
					var response = new List<ProveedorModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private ProveedorModel MapToValueProveedor(SqlDataReader reader)
		{
			return new ProveedorModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Contacto_ventas = reader["CONTACTO_VENTAS"].ToString(),
				Correo = reader["CORREO"].ToString(),
				Direccion = reader["DIRECCION"].ToString(),
				Id_proveedor = Convert.ToInt32(reader["ID_PROVEEDOR"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nombre = reader["NOMBRE"].ToString(),
				Nrc = reader["NRC"].ToString(),
				Telefono = reader["TELEFONO"].ToString()
			};
		}
	}

}
