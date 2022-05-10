using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryDetalle_orden_compraValue
	{
		private readonly string _connectionString;
		public RepositoryDetalle_orden_compraValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<Detalle_orden_compraModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDetalle_orden_compra_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<Detalle_orden_compraModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueDetalle_orden_compra(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(Detalle_orden_compraModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spDetalle_orden_compra_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_CANTIDAD", model.Cantidad));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_CREACION", model.Fecha_creacion));
					cmd.Parameters.Add(new SqlParameter("@p_ID_DETALLE_COMPRA", model.Id_detalle_compra));
					cmd.Parameters.Add(new SqlParameter("@p_ID_DIVISAS", model.Id_divisas));
					cmd.Parameters.Add(new SqlParameter("@p_ID_ORDEN_COMPRA", model.Id_orden_compra));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NUMERO_DETALLE", model.Numero_detalle));
					cmd.Parameters.Add(new SqlParameter("@p_PRECIO_UNITARIO", model.Precio_unitario));
					cmd.Parameters.Add(new SqlParameter("@p_VALOR_TOTAL", model.Valor_total));
					var response = new List<Detalle_orden_compraModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private Detalle_orden_compraModel MapToValueDetalle_orden_compra(SqlDataReader reader)
		{
			return new Detalle_orden_compraModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Cantidad = Convert.ToDecimal(reader["CANTIDAD"]),
				Fecha_creacion = Convert.ToDateTime(reader["FECHA_CREACION"]),
				Id_detalle_compra = Convert.ToInt32(reader["ID_DETALLE_COMPRA"]),
				Id_divisas = Convert.ToInt32(reader["ID_DIVISAS"]),
				Id_orden_compra = Convert.ToInt32(reader["ID_ORDEN_COMPRA"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Numero_detalle = reader["NUMERO_DETALLE"].ToString(),
				Precio_unitario = Convert.ToDecimal(reader["PRECIO_UNITARIO"]),
				Valor_total = Convert.ToDecimal(reader["VALOR_TOTAL"])
			};
		}
	}

}
