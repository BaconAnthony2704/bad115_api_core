using bad115_api_core.Models;
using bad115_api_core.Models.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryRequisicionValue
	{
		private readonly string _connectionString;
		private readonly IConfiguration config;
		public RepositoryRequisicionValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
			config = configuration;
		}
		public async Task<List<RequisicionModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spRequisicion_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<RequisicionModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueRequisicion(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task<List<RequisicionModel>> Guardar(RequisicionModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spRequisicion_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ESTADO", model.Estado));
					cmd.Parameters.Add(new SqlParameter("@p_FECHAINGRESADA", model.Fechaingresada));
					cmd.Parameters.Add(new SqlParameter("@p_FECHALIMITE", model.Fechalimite));
					cmd.Parameters.Add(new SqlParameter("@p_ID_SUCURSAL", model.Id_sucursal));
					cmd.Parameters.Add(new SqlParameter("@p_ID_USUARIO", model.Id_usuario));
					cmd.Parameters.Add(new SqlParameter("@p_IDREQUISICION", model.Idrequisicion));
					cmd.Parameters.Add(new SqlParameter("@p_USUARIOENCARGADO", model.Usuarioencargado));
					var response = new List<RequisicionModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					//return;
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueRequisicion(reader));
						}
					}
					return response;
				}
			}
		}

		public async Task GuardarRequisicionUDetalle(RequisicionUDetalleModel model)
		{
			
			//Guardamos requisicion
			var requision=await Guardar(model.requisicion);
			if (requision == null) return;
			//Guardamos detalle requision
			RepositoryDetallerequisicionValue detalleReq = new RepositoryDetallerequisicionValue(config);
            foreach (DetallerequisicionModel detalle in model.detalleRequisicion)
            {
				detalle.Idrequisicion = requision.FirstOrDefault().Idrequisicion;
				await detalleReq.Guardar(detalle);
            }
			return;


		}
		private RequisicionModel MapToValueRequisicion(SqlDataReader reader)
		{
			return new RequisicionModel
			{
				Estado = Convert.ToInt32(reader["ESTADO"]),
				Fechaingresada = Convert.ToDateTime(reader["FECHAINGRESADA"]),
				Fechalimite = Convert.ToDateTime(reader["FECHALIMITE"]),
				Id_sucursal = Convert.ToInt32(reader["ID_SUCURSAL"]),
				Id_usuario = Convert.ToInt32(reader["ID_USUARIO"]),
				Idrequisicion = Convert.ToInt32(reader["IDREQUISICION"]),
				Usuarioencargado = reader["USUARIOENCARGADO"].ToString()
			};
		}

		private DTORequisicionProducto MapToValueRequisicionProducto(SqlDataReader reader)
		{
			return new DTORequisicionProducto
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Descripcion_producto = reader["DESCRIPCION_PRODUCTO"].ToString(),
				Estado_requisicion = Convert.ToBoolean(reader["ESTADO_REQUISICION"]),
				Es_exportado = Convert.ToBoolean(reader["ES_EXPORTADO"]),
				FechaIngresada = Convert.ToDateTime(reader["FECHAINGRESADA"]),
				Fecha_vencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"]),
				Idrequisicion = Convert.ToInt32(reader["IDREQUISICION"].ToString()),
				Id_producto = Convert.ToInt32(reader["ID_PRODUCTO"].ToString()),
				Modificador_en = Convert.ToDateTime(reader["MODIFICADOR_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nombre_producto= Convert.ToString(reader["NOMBRE_PRODUCTO"]),
				Nombre_sucursal= Convert.ToString(reader["NOMBRE_SUCURSAL"]),
				Pais= Convert.ToString(reader["PAIS"]),
				Precio_minimo= Convert.ToDouble(reader["PRECIO_MINIMO"]),
				UsuarioEncargado= Convert.ToString(reader["USUARIOENCARGADO"])

			};
		}

		public async Task<List<DTORequisicionProducto>> ObtenerRequisicionProducto(IdentificadorModel model = null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("sp_RequisicionProducto_Obtener", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@id_requisicion", (model != null) ? model.id : null));
					var response = new List<DTORequisicionProducto>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueRequisicionProducto(reader));
						}
					}
					return response;
				}
			}
		}
	}

}
