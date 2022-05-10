using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryOrden_compraValue
	{
		private readonly string _connectionString;
		public RepositoryOrden_compraValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<Orden_compraModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spOrden_compra_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<Orden_compraModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueOrden_compra(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(Orden_compraModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spOrden_compra_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_APROBADA_POR", model.Aprobada_por));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_APROBACION", model.Fecha_aprobacion));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_ENTREGA", model.Fecha_entrega));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_ORDEN", model.Fecha_orden));
					cmd.Parameters.Add(new SqlParameter("@p_FECHA_REVISION", model.Fecha_revision));
					cmd.Parameters.Add(new SqlParameter("@p_ID_EMPRESA", model.Id_empresa));
					cmd.Parameters.Add(new SqlParameter("@p_ID_ORDEN_COMPRA", model.Id_orden_compra));
					cmd.Parameters.Add(new SqlParameter("@p_ID_PROVEEDOR", model.Id_proveedor));
					cmd.Parameters.Add(new SqlParameter("@p_IDREQUISICION", model.Idrequisicion));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NUMERO_ORDEN", model.Numero_orden));
					cmd.Parameters.Add(new SqlParameter("@p_OBSERVACIONES", model.Observaciones));
					cmd.Parameters.Add(new SqlParameter("@p_REVISADO_POR", model.Revisado_por));
					var response = new List<Orden_compraModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private Orden_compraModel MapToValueOrden_compra(SqlDataReader reader)
		{
			return new Orden_compraModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Aprobada_por = Convert.ToInt32(reader["APROBADA_POR"]),
				Fecha_aprobacion = Convert.ToDateTime(reader["FECHA_APROBACION"]),
				Fecha_entrega = Convert.ToDateTime(reader["FECHA_ENTREGA"]),
				Fecha_orden = Convert.ToDateTime(reader["FECHA_ORDEN"]),
				Fecha_revision = Convert.ToDateTime(reader["FECHA_REVISION"]),
				Id_empresa = Convert.ToInt32(reader["ID_EMPRESA"]),
				Id_orden_compra = Convert.ToInt32(reader["ID_ORDEN_COMPRA"]),
				Id_proveedor = Convert.ToInt32(reader["ID_PROVEEDOR"]),
				Idrequisicion = Convert.ToInt32(reader["IDREQUISICION"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Numero_orden = reader["NUMERO_ORDEN"].ToString(),
				Observaciones = reader["OBSERVACIONES"].ToString(),
				Revisado_por = Convert.ToInt32(reader["REVISADO_POR"])
			};
		}
	}

}
