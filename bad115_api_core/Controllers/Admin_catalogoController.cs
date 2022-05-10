using bad115_api_core.Models;
using bad115_api_core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bad115_api_core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Admin_catalogoController : ControllerBase
	{
		private readonly RepositoryAdmin_catalogoValue _repo;
		public Admin_catalogoController(RepositoryAdmin_catalogoValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<Admin_catalogoModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task Guardar([FromBody] Admin_catalogoModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
