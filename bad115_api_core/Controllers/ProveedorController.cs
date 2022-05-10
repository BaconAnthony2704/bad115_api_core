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
	public class ProveedorController : ControllerBase
	{
		private readonly RepositoryProveedorValue _repo;
		public ProveedorController(RepositoryProveedorValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<ProveedorModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task Guardar([FromBody] ProveedorModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
