using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryUsuarioValue
	{
		private readonly string _connectionString;
		public RepositoryUsuarioValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<UsuarioModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spUsuario_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<UsuarioModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueUsuario(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(UsuarioModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spUsuario_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_EMAIL", model.Email));
					cmd.Parameters.Add(new SqlParameter("@p_ID_USUARIO", model.Id_usuario));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					cmd.Parameters.Add(new SqlParameter("@p_PASSWORD", model.Password));
					cmd.Parameters.Add(new SqlParameter("@p_USUARIO", model.Usuario));
					var response = new List<UsuarioModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private UsuarioModel MapToValueUsuario(SqlDataReader reader)
		{
			return new UsuarioModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Email = reader["EMAIL"].ToString(),
				Id_usuario = Convert.ToInt32(reader["ID_USUARIO"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nombre = reader["NOMBRE"].ToString(),
				Password = reader["PASSWORD"].ToString(),
				Usuario = reader["USUARIO"].ToString()
			};
		}
	}

}
