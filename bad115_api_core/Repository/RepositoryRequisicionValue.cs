using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryRequisicionValue
	{
		private readonly string _connectionString;
		public RepositoryRequisicionValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
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


		public async Task Guardar(RequisicionModel model)
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
					return;
				}
			}
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
	}

}
