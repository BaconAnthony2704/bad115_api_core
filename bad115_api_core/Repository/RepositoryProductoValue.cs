using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryProductoValue
	{
		private readonly string _connectionString;
		public RepositoryProductoValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<ProductoModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spProducto_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<ProductoModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueProducto(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(ProductoModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spProducto_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_ES_EXPORTADO", model.Es_exportado));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_VENCIMIENTO", model.Fecha_vencimiento));
					cmd.Parameters.Add(new SqlParameter("@p_ID_ADMIN_CATALOGO", model.Id_admin_catalogo));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PRODUCTO", model.Id_producto));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADOR_EN", model.Modificador_en));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					cmd.Parameters.Add(new SqlParameter("@p_PRECIO_MINIMO", model.Precio_minimo));
					var response = new List<ProductoModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private ProductoModel MapToValueProducto(SqlDataReader reader)
		{
			return new ProductoModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Es_exportado = Convert.ToBoolean(reader["ES_EXPORTADO"]),
				Fecha_vencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"]),
				Id_admin_catalogo = Convert.ToInt32(reader["ID_ADMIN_CATALOGO"]),
				Id_producto = Convert.ToInt32(reader["ID_PRODUCTO"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Modificador_en = Convert.ToDateTime(reader["MODIFICADOR_EN"]),
				Nombre = reader["NOMBRE"].ToString(),
				Precio_minimo = Convert.ToDecimal(reader["PRECIO_MINIMO"])
			};
		}
	}

}
