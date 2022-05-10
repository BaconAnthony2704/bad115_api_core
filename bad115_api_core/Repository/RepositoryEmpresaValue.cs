using bad115_api_core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace bad115_api_core.Repository
{
	public class RepositoryEmpresaValue
	{
		private readonly string _connectionString;
		public RepositoryEmpresaValue(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Conexion");
		}
		public async Task<List<EmpresaModel>> Obtener(IdentificadorModel model=null)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spEmpresa_ver", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_id", (model != null) ? model.id : null));
					var response = new List<EmpresaModel>();
					await sql.OpenAsync();
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							response.Add(MapToValueEmpresa(reader));
						}
					}
					return response;
				}
			}
		}


		public async Task Guardar(EmpresaModel model)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("spEmpresa_guardar", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@p_ACTIVO", model.Activo));
					cmd.Parameters.Add(new SqlParameter("@p_DIRECCION_FISCAL", model.Direccion_fiscal));
					cmd.Parameters.Add(new SqlParameter("@p_EMAIL", model.Email));
					cmd.Parameters.Add(new SqlParameter("@p_ID_EMPRESA", model.Id_empresa));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_EN", model.Modificado_en));
					cmd.Parameters.Add(new SqlParameter("@p_MODIFICADO_POR", model.Modificado_por));
					cmd.Parameters.Add(new SqlParameter("@p_NIT", model.Nit));
					cmd.Parameters.Add(new SqlParameter("@p_NOMBRE", model.Nombre));
					cmd.Parameters.Add(new SqlParameter("@p_TELEFONO", model.Telefono));
					var response = new List<EmpresaModel>();
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return;
				}
			}
		}
		private EmpresaModel MapToValueEmpresa(SqlDataReader reader)
		{
			return new EmpresaModel
			{
				Activo = Convert.ToBoolean(reader["ACTIVO"]),
				Direccion_fiscal = reader["DIRECCION_FISCAL"].ToString(),
				Email = reader["EMAIL"].ToString(),
				Id_empresa = Convert.ToInt32(reader["ID_EMPRESA"]),
				Modificado_en = Convert.ToDateTime(reader["MODIFICADO_EN"]),
				Modificado_por = Convert.ToInt32(reader["MODIFICADO_POR"]),
				Nit = reader["NIT"].ToString(),
				Nombre = reader["NOMBRE"].ToString(),
				Telefono = reader["TELEFONO"].ToString()
			};
		}
	}

}
