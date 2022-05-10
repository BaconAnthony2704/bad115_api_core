

using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryAdmin_catalogoValue
	{
		private readonly string _connectionString;
		public RepositoryAdmin_catalogoValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<Admin_catalogoModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spAdmin_catalogo_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<Admin_catalogoModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueAdmin_catalogo(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(Admin_catalogoModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spAdmin_catalogo_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_CODIGO", model.Codigo));
					cmd.Parameters.Add(new SqlParameter("@p_DESCRIPCION", model.Descripcion));
					cmd.Parameters.Add(new SqlParameter("@p_ID_ADMIN_CATALOGO", model.Id_admin_catalogo));
					cmd.Parameters.Add(new SqlParameter("@p_IDEMPLEADO", model.Idempleado));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_TIPO", model.Tipo));
					var response = new List<Admin_catalogoModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private Admin_catalogoModel MapToValueAdmin_catalogo(SqlDataReader reader)
		{
			return new Admin_catalogoModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Codigo = reader["CODIGO"].ToString(),
				Descripcion = reader["DESCRIPCION"].ToString(),
				Id_admin_catalogo = Convert.ToInt32(reader["ID_ADMIN_CATALOGO"]),
				Idempleado = Convert.ToInt32(reader["IDEMPLEADO"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Tipo = reader["TIPO"].ToString()
			};
		}
	}


}
